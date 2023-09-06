import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-palestrantes',
  templateUrl: './palestrantes.component.html',
  styleUrls: ['./palestrantes.component.scss']
})
export class PalestrantesComponent implements OnInit {

  public tituloPagina: string = 'Palestrantes';

  constructor() { }

  ngOnInit() {
  }

}
