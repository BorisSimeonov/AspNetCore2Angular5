import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { QuizListComponent } from './components/quiz/quiz-list/quiz-list.component';
import { QuizComponent } from "./components/quiz/quiz/quiz.component";
import { QuizEditComponent } from "./components/quiz/quiz-edit/quiz-edit.component";
import { AboutComponent } from "./components/about/about.component";
import { LoginComponent } from "./components/login/login.component";
import { PageNotFoundComponent } from "./components/pageNotFound/pagenotfound.component";

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        HomeComponent,
        NavMenuComponent,
        QuizListComponent,
        QuizComponent,
        QuizEditComponent,
        AboutComponent,
        PageNotFoundComponent
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
            { path: '**', component: PageNotFoundComponent }
        ])
    ]
})

export class AppModuleShared {
}
