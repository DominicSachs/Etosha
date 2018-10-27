import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing';
import { AuthModule } from './auth/auth.module';
import { LanguageChoiceModule } from './shared/components/language-choice/language-choice.module';
import { AuthGuard } from './shared/guards/auth.guard';
import { TokenInterceptor } from './shared/interceptors/token.interceptor';
import { MaterialModule } from './shared/modules/material.module';
import { SharedModule } from './shared/modules/shared.module';
import { AuthService } from './shared/services/auth.service';
import { StocksModule } from './stocks/stocks.module';
import { UsersModule } from './users/users.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    MaterialModule,
    FlexLayoutModule,
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
