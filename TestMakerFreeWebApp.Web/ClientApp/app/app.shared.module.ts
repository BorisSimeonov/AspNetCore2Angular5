import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { LoginComponent } from "./components/login/login.component";
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PageNotFoundComponent } from "./components/pageNotFound/pagenotfound.component";
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from "./components/about/about.component";
import { QuizListComponent } from './components/quiz/quiz-list/quiz-list.component';
import { QuizComponent } from "./components/quiz/quiz/quiz.component";
import { QuizEditComponent } from "./components/quiz/quiz-edit/quiz-edit.component";
import { QuestionListComponent } from "./components/question/question-list/question-list.component";
import { QuestionEditComponent } from "./components/question/question-edit/question-edit.component";
import { AnswerListComponent } from "./components/answer/answer-list/answer-list.component";
import { AnswerEditComponent } from "./components/answer/answer-edit/answer-edit.component";

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        HomeComponent,
        NavMenuComponent,
        AboutComponent,
        PageNotFoundComponent,

        // Quiz components
        QuizListComponent,
        QuizComponent,
        QuizEditComponent,

        // Question components
        QuestionListComponent,
        QuestionEditComponent,

        // Answer components
        AnswerListComponent,
        AnswerEditComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'login', component: LoginComponent },
            { path: 'home', component: HomeComponent },
            { path: 'about', component: AboutComponent },
            { path: 'quiz/edit/:id', component: QuizEditComponent },
            { path: 'quiz/create', component: QuizEditComponent },
            { path: 'quiz/:id', component: QuizComponent },
            { path: 'quiz-list', component: QuizListComponent },
            { path: 'question/edit/:id', component: QuestionEditComponent },
            { path: 'question/create/:id', component: QuestionEditComponent },
            { path: 'answer/edit/:id', component: AnswerEditComponent },
            { path: 'answer/create/:id', component: AnswerEditComponent },
            { path: '**', component: PageNotFoundComponent }
        ])
    ]
})

export class AppModuleShared {
}
