import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatUiComponent } from './chat-ui/chat-ui.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';

const routes: Routes = [
  {path:'', redirectTo:'login', pathMatch:'full'}, 
  {path:'login', component:LoginComponent},
  {path:'registration', component:RegistrationComponent},
  {path:'chatui', component:ChatUiComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
