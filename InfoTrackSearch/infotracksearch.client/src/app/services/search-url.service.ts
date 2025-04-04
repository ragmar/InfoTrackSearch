import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchUrlRequest } from '../models/search-url-requet';
import { environment } from '../../enviroments/environment';
import { SearchUrlResponse } from '../models/search-url-response';

@Injectable()
export default class SearchUrlService {
  constructor(private readonly http: HttpClient) {}

  searchUrl(searchUrlRequest: SearchUrlRequest): Observable<SearchUrlResponse> {
    const apiEndpointUrl = environment.SearchUrl
        .replace('{searchTerm}', searchUrlRequest.SearchTerm)
        .replace('{urlToSearch}', searchUrlRequest.UrlToSearch);

    return this.http.get<SearchUrlResponse>(apiEndpointUrl);
  }
}
