import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contatos',
  templateUrl: './contatos.component.html',
  styleUrls: ['./contatos.component.scss']
})
export class ContatosComponent implements OnInit {

  public tituloPagina: string = 'Contatos';

  constructor() { }

  ngOnInit() {
  }

}
