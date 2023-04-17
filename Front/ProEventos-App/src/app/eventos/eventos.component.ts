import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public eventos: any = [];
  public eventosFiltrados: any = [];
  widthImg: number = 50;
  marginImg: number = 2;
  exibirImagem: boolean = true;
  private _filtro: string = '';

  public get filtro(): string {
    return this._filtro;
  }
  public set filtro(value: string) {
    this._filtro = value;
    this.eventosFiltrados = this.filtro
      ? this.filtrarEventos(this.filtro)
      : this.eventos;
  }

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get('https://localhost:7082/api/evento').subscribe(
      (response) => {
        this.eventos = response;
        this.eventosFiltrados = this.eventos;
      },
      (error) => console.log(error)
    );
  }

  private filtrarEventos(filtro: string): any {
    filtro = filtro.toLocaleLowerCase();
    return this.eventos.filter(
      // (evento: any) ou =>
      (evento: { tema: string; local: string; lote: string; }) =>
        evento.tema.toLocaleLowerCase().indexOf(filtro) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtro) !== -1 ||
        evento.lote == filtro
    );
  }
}
