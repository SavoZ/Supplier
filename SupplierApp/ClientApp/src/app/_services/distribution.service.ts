import { Injectable } from '@angular/core';
import { API_ENDPOINT } from '../_constants/url.constants';
import { HttpClient } from '@angular/common/http';
import { DistributionModel } from '../_models/distributionPostModel';

@Injectable({
  providedIn: 'root'
})
export class DistributionService {

  constructor(private http: HttpClient) { }

  public save(model: DistributionModel) {
    this.http.post(API_ENDPOINT + 'Distribution/SaveDistribution', model).toPromise();
  }
}
