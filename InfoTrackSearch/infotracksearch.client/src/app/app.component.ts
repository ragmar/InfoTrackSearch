import { Component, OnDestroy } from '@angular/core';
import { SearchUrlRequest } from './models/search-url-requet';
import SearchUrlService from './services/search-url.service';
import { Subscription } from 'rxjs';
import { ApiError } from './models/api-error';
import { HttpResponse } from '@angular/common/http';
import { SearchUrlResponse } from './models/search-url-response';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnDestroy {
  forecasts: WeatherForecast[] = [];
  searchTerm: string = 'land registry search';
  urlToSearch: string = 'www.infotrack.co.uk';
  searchedUrl: string;
  error: ApiError;
  urlPositions: string = '';
  private subscription: Subscription;


  constructor(private searchUrlService: SearchUrlService) {}

  search() {
    let searchUrlRequest = new SearchUrlRequest();
    searchUrlRequest.SearchTerm = this.searchTerm;
    searchUrlRequest.UrlToSearch = this.urlToSearch;
    this.searchedUrl = this.urlToSearch;
    this.urlPositions = '';

    if (!searchUrlRequest.SearchTerm || !searchUrlRequest.UrlToSearch) {
      return;
    }

    this.subscription = this.searchUrlService.searchUrl(searchUrlRequest)
    
    .subscribe({
      next: (response: SearchUrlResponse) => {
        this.urlPositions = response.data;
      },
      error: (httpResponse) => {
        this.error = new ApiError(httpResponse.error.Message ?? httpResponse.error, httpResponse.status)
      },
      complete: () => {
      }
    });
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}
