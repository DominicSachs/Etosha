import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app.routing';
import { UsersModule } from './users/users.module';
import { AuthModule } from './auth/auth.module';
import { AuthGuard } from './shared/guards/auth.guard';
import { AuthService } from './shared/services/auth.service';
import { TokenInterceptor } from './shared/interceptors/token.interceptor';
import { StocksModule } from './stocks/stocks.module';
import { AppComponent } from './app.component';
import { LanguageChoiceModule } from './shared/components/language-choice/language-choice.module';
import { SharedModule } from './shared/modules/shared.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    UsersModule,
    AuthModule,
    StocksModule,
    LanguageChoiceModule
  ],
  providers: [
    AuthGuard,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
