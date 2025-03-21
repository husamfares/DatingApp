import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { PaginatedResult } from '../_models/Pagination';
import { Message } from '../_models/message';
import { setPaginatedRespnose, setPaginationHeaders } from './PaginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  PaginatedResult = signal<PaginatedResult<Message[]> | null>(null);

    getMessages(pageNumber: number , pageSize: number , container: string)
    {
      let params = setPaginationHeaders(pageNumber , pageSize);

      params = params.append('Container' , container);

      return this.http.get<Message[]>(this.baseUrl+'messages' , {observe: 'response' , params})
      .subscribe({
        next: response => setPaginatedRespnose(response , this.PaginatedResult)
      })
      
    }

    getMessageThread(username: string)
    {
      return this.http.get<Message[]>(this.baseUrl+ 'messages/thread/' + username);
    }

    sendMessage(username: string, content: string)
    {
      return this.http.post<Message>(this.baseUrl+ 'messages', {recipientUserName: username ,content})
    }

    deleteMessage(id: number)
    {
      return this.http.delete(this.baseUrl + 'messages/' + id);
    }
}
