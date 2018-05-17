import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router"

@Component({
    selector: "quiz",
    templateUrl: './quiz.component.html',
    styleUrls: ['./quiz.component.css']
})

export class QuizComponent {
    quiz: Quiz;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {

        this.quiz = <Quiz>{};

        var id = activatedRoute.snapshot.params["id"]
        console.log(id);

        if (id) {
            var url = `${this.baseUrl}api/quiz/${id}`;

            http.get<Quiz>(url)
                .subscribe(
                    result => { this.quiz = result; },
                    error => { console.error(error); });
        }
        else {
            console.log("Invalid id. Routing back to home.");
        }
    }
}