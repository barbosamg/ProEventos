import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import * as moment from 'moment';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Lote } from '@app/models/Lote';
import { LoteService } from '@app/services/lote.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  // form!: FormGroup;
  // evento: Evento = {} as Evento;
  form: FormGroup = new FormGroup({});
  evento = {} as Evento;
  public spinnerSalvar: boolean = false;
  public modoEdicao: boolean = false;
  private eventoId: number = 0;
  modalRef?: BsModalRef;
  loteAtual: Lote = {} as Lote;
  indiceLoteAtual: number = 0;

  get f(): any {
    return this.form.controls;
  }

  //deixo o get pois quero trabalhar com propriedade ao inves de função
  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY HH:MM',
      containerClass: 'theme-dark-blue',
    };
  }

  get bsConfigLote(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-dark-blue',
    };
  }

  get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }

  constructor(
    private formBuilder: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRoute: ActivatedRoute,
    private eventoService: EventoService,
    private loteService: LoteService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router,
    private modalService: BsModalService
  ) {
    localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.validation();
    this.carregarEvento();
  }

  public validation(): void {
    this.form = this.formBuilder.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      quantidadePessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: ['', Validators.required],
      lotes: this.formBuilder.array([]),
    });
  }

  public adicionarLote(): void {
    this.lotes.push(this.criarLote({ id: 0 } as Lote));
  }

  private criarLote(lote: Lote): FormGroup {
    return this.formBuilder.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      quantidade: [lote.quantidade, Validators.required],
      preco: [lote.preco, Validators.required],
      dataInicio: [lote.dataInicio],
      dataFinal: [lote.dataFinal],
    });
  }

  public carregarEvento(): void {
    this.eventoId = Number(this.activatedRoute.snapshot.paramMap.get('id'));

    if (this.eventoId !== null && this.eventoId !== 0) {
      this.spinner.show();
      // ou: this.eventoService.getEventoById(parseInt(eventoIdRouter)); // o + converte para inteiro
      this.modoEdicao = true;
      this.eventoService.getEventoById(this.eventoId).subscribe(
        (eventoResponse: Evento) => {
          // this.evento = Object.assign({}, eventoResponse); ou:
          this.evento = { ...eventoResponse };
          const dateMoment: moment.Moment = moment(
            this.evento.dataEvento,
            'DD/MM/YYYY hh:mm'
          );
          const dateJS: Date = dateMoment.toDate();
          this.evento.dataEvento = dateJS;
          this.form.patchValue(this.evento);
          this.carregarLote();
          // this.carregarLotes();
        },
        (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar carregar evento', 'Erro!');
          console.log('Erro ao buscar evento por id: ', error);
        },
        () => {
          this.spinner.hide();
        }
      );
    }
  }

  private carregarLote(): void {
    this.evento.lotes.forEach((lote) => {
      // FORMATAR A DATA INICIO
      let dateMomentInicio: moment.Moment = moment(
        lote.dataInicio,
        'YYYY-MM-DD'
      );
      let dateJSInicio: Date = dateMomentInicio.toDate();
      lote.dataInicio = dateJSInicio;

      // FORMATAR A DATA FINAL
      let dateMomentFinal: moment.Moment = moment(
        lote.dataFinal,
        'YYYY-MM-DD'
      );
      let dateJSFinal: Date = dateMomentFinal.toDate();
      lote.dataFinal = dateJSFinal;

      //ADICIONAR NO ARRAY LOTES
      this.lotes.push(this.criarLote(lote));
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoFomr: FormControl | AbstractControl | null): object {
    return { 'is-invalid': campoFomr?.errors && campoFomr?.touched };
  }

  public salvarEvento(): void {
    this.spinnerSalvar = true;
    if (this.form.valid) {
      this.evento = { ...this.form.value };
      this.eventoService.postEvento(this.evento).subscribe(
        (response: Evento) => {
          this.toastr.success('Evento salvo com sucesso', 'Sucesso!');
          this.router.navigate([`eventos/detalhe/${response.id}`]);
        },
        (error) => {
          console.error(error);
          this.toastr.error('Erro ao salvar evento' + error.message, 'Erro!');
          this.spinnerSalvar = false;
        },
        () => {
          this.spinnerSalvar = false;
        }
      );
    }
  }

  public alterarEvento(): void {
    this.spinnerSalvar = true;
    if (this.form.valid) {
      this.evento = { id: this.evento.id, ...this.form.value };
      this.eventoService.putEvento(this.evento).subscribe(
        (response) => {
          this.toastr.success('Evento alterado com sucesso', 'Sucesso!');
        },
        (error) => {
          console.error(error);
          this.toastr.error('Erro ao alterar evento', 'Erro!');
        },
        () => {
          this.spinnerSalvar = false;
        }
      );
    }
  }

  public salvarLotes(): void {
    if (this.form.controls.lotes.valid) {
      this.spinnerSalvar = true;
      this.loteService
        .saveLote(this.eventoId, this.form.value.lotes)
        .subscribe(
          () => {
            this.toastr.success('Lotes salvos com sucesso!', 'Sucesso!');
            // this.lotes.reset();
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar salvar lotes', 'Erro');
            console.error(error);
          }
        )
        .add(() => (this.spinnerSalvar = false));
    }
  }

  public removerLote(
    modalDeleteLote: TemplateRef<any>,
    indiceLote: number
  ): void {
    this.loteAtual = this.lotes.at(indiceLote).value;
    this.indiceLoteAtual = indiceLote;
    this.modalRef = this.modalService.show(modalDeleteLote, {
      class: 'modal-sm',
    });
  }

  public confirmDeleteLote(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.loteService
      .deleteLote(this.eventoId, this.loteAtual.id)
      .subscribe(
        () => {
          this.lotes.removeAt(this.indiceLoteAtual);
          this.toastr.success('Lote deletado com sucesso', 'Sucesso!');
        },
        (error: any) => {
          this.toastr.error(
            'Erro ao tentar excluir lote' + this.loteAtual.id,
            'Erro'
          );
          console.error(error);
        }
      )
      .add(() => this.spinner.hide());
  }

  public declineDeleteLote(): void {
    this.modalRef?.hide();
  }

  public getTituloLote(tituloLote: string | null): string{
    return tituloLote === null || tituloLote === '' ? 'Nome lote' : tituloLote;
  }
}

// public carregarLotes(): void{
//   this.loteService.getLotesByEventoId(this.eventoId).subscribe(
//     (lotesRetorno: Lote[]) => {
//       lotesRetorno.forEach(lote => {
//         this.lotes.push(this.criarLote(lote));
//       });
//     },
//     (error: any) => {
//       this.toastr.error('Erro ao tentar carregar lotes', 'Erro')
//       console.error(error);
//     },
//   ).add(() => this.spinner.hide());
// }
