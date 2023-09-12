import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '@app/services/evento.service';
import { Evento } from '@app/models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public widthImg: number = 50;
  public marginImg: number = 2;
  public exibirImagem: boolean = true;
  private _filtro: string = '';

  modalRef?: BsModalRef;

  public get filtro(): string {
    return this._filtro;
  }

  public set filtro(value: string) {
    this._filtro = value;
    this.eventosFiltrados = this.filtro
      ? this.filtrarEventos(this.filtro)
      : this.eventos;
  }

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private router: Router) {}

  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe({
      next: (response: Evento[]) => {
        this.eventos = response;
        this.eventosFiltrados = this.eventos;
      },
      error: (error) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar os eventos', 'Erro!');
        console.log(error);
      },
      complete: () => {
          this.spinner.hide();
      }
  });
  }

  private filtrarEventos(filtro: string): Evento[] {
    filtro = filtro.toLocaleLowerCase();
    return this.eventos.filter(
      // (evento: any) ou =>
      // (evento: { tema: string; local: string; lote: string; })
      (evento: Evento) =>
        evento.tema.toLocaleLowerCase().indexOf(filtro) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtro) !== -1
    );
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('Evento foi deletado com sucesso', 'Deletado!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(id: number): void{
    this.router.navigate([`/eventos/detalhe/${id}`])
  }
}
