import { Component } from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent {
    options: string[] = ["Latest", "By Title", "Random"];
    selectedOption: string;

    constructor() {
        this.selectedOption = this.options[0];
    }
}