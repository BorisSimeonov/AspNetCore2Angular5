import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: "quiz-edit",
    templateUrl: './quiz-edit.component.html',
    styleUrls: ['./quiz-edit.component.css']
})

export class QuizEditComponent implements OnInit {
    title!: string;
    quiz: Quiz;

    editMode: boolean;

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {

        this.editMode = false;
        this.quiz = <Quiz>{};
    }

    ngOnInit(): void {
        this.title = "Create a new Quiz";
        var id = +this.activatedRoute.snapshot.params["id"];

        if (id) {
            this.editMode = true;
            var url = this.baseUrl + `api/quiz/${id}`;
            this.GetQuiz(id, url);
        }
    }

    GetQuiz(id: number, url: string) {
        this.http.get<Quiz>(url).subscribe(result => {
            this.title = `Edit - ${result.Title}`;
            this.quiz = result;
        }, error => console.error(error));
    }

    onSubmit(quiz: Quiz) {
        var url = `${this.baseUrl}api/quiz`;

        if (this.editMode) {
            this.http
                .post<Quiz>(url, quiz)
                .subscribe(res => {
                    let q = res;
                    console.log(`Quiz ${q.Id} has been updated.`);
                    this.onBack();
                }, error => console.log(error));
        }
        else {
            this.http
                .put<Quiz>(url, quiz)
                .subscribe(res => {
                    let q = res;
                    console.log(`Quiz ${q.Id} has been created.`);
                    this.onBack();
                }, error => console.log(error));
        }
    }

    onBack() {
        this.router.navigate(['home']);
    }
}