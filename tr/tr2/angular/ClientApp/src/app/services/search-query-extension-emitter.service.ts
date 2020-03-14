import { Injectable } from '@angular/core';
import { SearchQueryExtension } from '../models/ISearchQueryExtension';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchQueryExtensionEmitterService {

  private data = new BehaviorSubject<SearchQueryExtension>(new SearchQueryExtension());
  obs = this.data.asObservable();

  constructor() { }

  emit(searchQueryExtension: SearchQueryExtension) {
    this.data.next(searchQueryExtension);
  }
}
