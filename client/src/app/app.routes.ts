import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { DetailComponent } from './members/detail/detail.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guard/auth.guard';

export const routes: Routes = [
    {path : '', component :HomeComponent},
    {
        path : '',
        runGuardsAndResolvers : 'always',
        canActivate : [authGuard],
        children : [
            {path : 'members', component :MemberListComponent},
            {path : 'members/:id', component :DetailComponent},
            {path : 'lists', component :ListsComponent},
            {path : 'messages', component :MessagesComponent},

        ]
    },
    
    {path : '**', component :HomeComponent , pathMatch : "full"},

];
