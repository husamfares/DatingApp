import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { DetailComponent } from './members/detail/detail.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guard/auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { preventsUnsavedChangsGuard } from './_guard/prevents-unsaved-changs.guard';

export const routes: Routes = [
    {path : '', component :HomeComponent},
    {
        path : '',
        runGuardsAndResolvers : 'always',
        canActivate : [authGuard],
        children : [
            {path : 'members', component :MemberListComponent},
            {path : 'members/:username', component :DetailComponent},
            { path: 'member/edit', component: MemberEditComponent , canDeactivate: [preventsUnsavedChangsGuard] },
            {path : 'lists', component :ListsComponent},
            {path : 'messages', component :MessagesComponent},

        ]
    },
    {path : "errors" , component :TestErrorsComponent},
    {path : "not-found" , component :NotFoundComponent},
    {path : "server-error" , component :ServerErrorComponent},

    {path : '**', component :HomeComponent , pathMatch : "full"},

];
