import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: "question-edit",
    templateUrl: "./question-edit.component.html",
    styleUrls: ["./question-edit.component.css"]
})

export class QuestionEditComponent {
    title: string;
    question: Question;
    editMode: boolean;

    constructor(private activatedRoute: ActivatedRoute,
        private http: HttpClient,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string) {
        this.title = "";
        this.question = <Question>{};

        let id = +this.activatedRoute.snapshot.params["id"];

        this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

        if (this.editMode) {
            this.getQuestion(id);
        } else {
            this.question.QuizId = id;
            this.title = "Create a new Question";
        }
    }

    getQuestion(id: number) {
        let url = `${this.baseUrl}api/question/${id}`;
        this.http
            .get<Question>(url)
            .subscribe(result => {
                this.question = result
                this.title = `Edit - ${result.Text}`
            }, error => console.log(error));
    }

    onSubmit(question: Question) {
        let url = `${this.baseUrl}api/question`;

        if (this.editMode) {
            this.http
                .post<Question>(url, question)
                .subscribe(result => {
                    console.log(`Question ${result.Id} has been updated.`);
                    this.router.navigate(["quiz/edit", result.QuizId]);
                }, error => console.log(error));
        } else {
            this.http
                .put<Question>(url, question)
                .subscribe(result => {
                    console.log(`Question ${result.Id} has been created.`);
                    this.router.navigate(["quiz/edit", result.QuizId]);
                }, error => console.log(error));
        }
    }

    onBack() {
        this.router.navigate(["quiz/edit", this.question.QuizId]);
    }
}