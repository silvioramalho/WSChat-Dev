import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './views/register/register.component';
import { RoomsComponent } from './views/rooms/rooms.component';
import { ChatComponent } from './chat.component';
import { PainelComponent } from './views/painel/painel.component';
import { RegisterGuardService } from './helpers/services/register-guard.service';

const routes: Routes = [
  {
    path: '',
    component: ChatComponent,
    children: [
      {
        path: '',
        redirectTo: 'register',
        pathMatch: 'full'
      },
      {
        path: 'register',
        component: RegisterComponent,
        canActivate: [RegisterGuardService]
      },
      {
        path: 'rooms',
        component: RoomsComponent,
      },
      {
        path: 'painel',
        component: PainelComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ChatRoutingModule {}
