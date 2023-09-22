import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';
import { take } from 'rxjs/operators';

/* pipe(take1)) serve para desinscrever o observable, take(1) dentro do pipe vai permitir que o observable seja chamado apenas 1 vez antes de ser completado,
isso quer dizer que, mesmo que a sua inscrição (subscribe) ainda esteja lá no seu observable, o take(quatidade de vezes)
vai completar o observable impedindo ele de utilizar mais memória do seu browser de forma indesejada.
https://rxjs.dev/api/operators/take*/

// @Injectable({
//   providedIn: 'root'
// })
@Injectable()
export class EventoService {
  private baseURL: string = 'https://localhost:7082/api/evento';

  constructor(private http: HttpClient) {}

  public getEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL).pipe(take(1));
  }

  public getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http
      .get<Evento[]>(`${this.baseURL}/${tema}/tema`)
      .pipe(take(1));
  }

  public getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public postEvento(evento: Evento): Observable<Evento> {
    return this.http.post<Evento>(this.baseURL, evento).pipe(take(1));
  }

  public putEvento(evento: Evento): Observable<Evento> {
    return this.http
      .put<Evento>(`${this.baseURL}/${evento.id}`, evento)
      .pipe(take(1));
  }

  public deleteEvento(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }
}
