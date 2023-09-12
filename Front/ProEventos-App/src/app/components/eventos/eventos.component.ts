import { Component, OnInit, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {

  public tituloPagina: string = 'Eventos';
  public iconeTitlo: string = 'fa fa-calendar-alt';

  ngOnInit(): void {}

}
