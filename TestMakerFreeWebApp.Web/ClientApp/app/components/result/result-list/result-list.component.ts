import { Component, Inject, Input, OnChanges, SimpleChanges } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";

@Component({
    selector: "result-list",
    templateUrl: "result-list.component.html",
    styleUrls: ["result-list.component.css"]
})

export class ResultListComponent implements OnChanges {
    results: Result[];
    @Input() quiz: Quiz;

    constructor(private http: HttpClient,
        private router: Router,
        @Inject("BASE_URL") private baseUrl: string) {

        this.results = [];
        this.quiz = <Quiz>{};
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (typeof changes["quiz"] !== "undefined") {
            let change = changes["quiz"];

            if (!change.isFirstChange()) {
                this.loadData();
            }
        }
    }

    loadData() {
        let url = `${this.baseUrl}api/result/all/${this.quiz.Id}`;
        this.http
            .get<Result[]>(url)
            .subscribe(result => {
                this.results = result;
            }, error => console.log(error));
    }

    onDelete(result: Result) {
        let url = `${this.baseUrl}api/result/${result.Id}`;

        this.http
            .delete(url)
            .subscribe(result => {
                this.loadData();
            }, error => console.log(error));
    }

    onEdit(result: Result) {
        this.router.navigate(["result/edit", result.Id]);
    }

    onCreate(result: Result) {
        this.router.navigate(["result/create", this.quiz.Id]);
    }
}