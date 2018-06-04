import { Component, OnInit, Input, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
    selector: "result-edit",
    templateUrl: "result-edit.component.html",
    styleUrls: ["result-edit.component.css"]
})

export class ResultEditComponent {
    editMode: boolean;
    result: Result;
    title: string;

    constructor(private http: HttpClient,
        private router: Router,
        @Inject("BASE_URL") private baseUrl: string,
        private activatedRoute: ActivatedRoute) {

        this.result = <Result>{};
        this.title = "";
        let id = +this.activatedRoute.snapshot.params["id"];

        this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

        if (this.editMode) {
            this.getResult(id);
        } else {
            this.result.QuizId = id;
            this.title = "Create a new Result";
        }
    }

    getResult(id: number) {
        var url = `${this.baseUrl}api/result/${id}`;
        this.http
            .get<Result>(url)
            .subscribe(result => {
                this.result = result;
                this.title = `Edit - ${result.Text}`;
            }, error => console.log(error));
    }

    onSubmit(result: Result) {
        let url = `${this.baseUrl}api/result`;

        if (this.editMode) {
            this.http
                .post<Result>(url, result)
                .subscribe(result => {
                    console.log(`Result ${result.Id} has been updated.`);
                    this.router.navigate(["quiz/edit", result.QuizId]);
                }, error => console.log(error));
        } else {
            this.http
                .put<Result>(url, result)
                .subscribe(result => {
                    console.log(`Result ${result.Id} has been created.`);
                    this.router.navigate(["quiz/edit", result.QuizId]);
                }, error => console.log(error));
        }
    }

    onBack() {
        this.router.navigate(["quiz/edit", this.result.QuizId]);
    }
}