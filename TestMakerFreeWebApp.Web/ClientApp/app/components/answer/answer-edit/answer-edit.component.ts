import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: "answer-edit",
    templateUrl: "./answer-edit.component.html",
    styleUrls: ["./answer-edit.component.css"]
})

export class AnswerEditComponent {
    title: string;
    answer: Answer;
    editMode: boolean;

    constructor(private activatedRoute: ActivatedRoute,
        private http: HttpClient,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string) {

        this.title = "";
        this.answer = <Answer>{};

        let id = +this.activatedRoute.snapshot.params["id"];

        this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

        if (this.editMode) {
            this.getAnswers(id);
        } else {
            this.answer.QuestionId = id;
            this.title = "Create a new Answer";
        }
    }

    getAnswers(id: number) {
        let url = `${this.baseUrl}api/answer/${id}`;
        this.http.get<Answer>(url)
            .subscribe(result => {
                this.answer = result
                this.title = `Edit - ${result.Text}`
            }, error => console.log(error));
    }

    onSubmit(answer: Answer) {
        let url = `${this.baseUrl}api/answer`;

        if (this.editMode) {
            this.http
                .post<Answer>(url, answer)
                .subscribe(result => {
                    console.log(`Answer ${result.Id} has been updated.`);
                    this.router.navigate(["question/edit", result.QuestionId]);
                }, error => console.log(error));
        } else {
            this.http
                .put<Answer>(url, answer)
                .subscribe(result => {
                    console.log(`Answer ${result.Id} has been created.`);
                    this.router.navigate(["question/edit", result.QuestionId]);
                }, error => console.log(error));
        }
    }

    onBack() {
        this.router.navigate(["question/edit", this.answer.QuestionId]);
    }
}