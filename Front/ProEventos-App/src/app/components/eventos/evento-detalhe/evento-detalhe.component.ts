import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
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

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  // form!: FormGroup;
  form: FormGroup = new FormGroup({});
  // evento: Evento = {} as Evento;
  evento = {} as Evento;
  public spinnerSalvar: boolean = false;
  public modoEdicao: boolean = false;

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

  constructor(
    private formBuilder: FormBuilder,
    private localeService: BsLocaleService,
    private router: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
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
    });
  }

  public carregarEvento(): void {
    const eventoIdRouter = this.router.snapshot.paramMap.get('id');

    if (eventoIdRouter !== null) {
      this.spinner.show();
      // ou: this.eventoService.getEventoById(parseInt(eventoIdRouter)); // o + converte para inteiro
      this.modoEdicao = true;
      this.eventoService.getEventoById(+eventoIdRouter).subscribe(
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

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoFomr: FormControl): object {
    return { 'is-invalid': campoFomr.errors && campoFomr.touched };
  }

  public salvarEvento(): void {
    this.spinnerSalvar = true;
    if (this.form.valid) {
      this.evento = { ...this.form.value };
      this.eventoService.postEvento(this.evento).subscribe(
        (response) => {
          this.toastr.success('Evento salvo com sucesso', 'Sucesso!');
        },
        (error) => {
          console.error(error);
          this.toastr.error('Erro ao salvar evento', 'Erro!');
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
}
