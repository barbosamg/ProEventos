import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  public tituloPagina: string = 'Perfil';
  form!: FormGroup;

  get f(): any{
    return this.form.controls;
  }

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  onSubmit(event: any): any{
    if(this.form.invalid) return;
  }

  private validation(): void{
    const formOptions: AbstractControlOptions = { validators: ValidatorField.MustMatch('senha', 'confirmeSenha') };

    this.form = this.formBuilder.group({
      titulo: ['', Validators.required],
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', Validators.required],
      funcao: ['', Validators.required],
      descricao: ['', Validators.required],
      senha: ['', [Validators.required, Validators.minLength(6)]],
      confirmeSenha: ['', Validators.required],
    }, formOptions);
  }

  public resetForm(event: any): void{
    event.preventDefault();
    this.form.reset();
  }

}
