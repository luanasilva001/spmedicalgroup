import axios from "axios";
import { relativeTimeThreshold } from "moment";
import { Component } from "react";

export default class ConsultasMedico extends Component {
  constructor(props) {
    super(props);
    this.state = {
      listaMedico: [],
      idConsultaAlterada: 0,
      descricao: "",
    };
  }

  buscarConsultasMedico = () => {
    axios("http://localhost:5000/api/consulta/medico-consulta", {
      headers: {
        Authorization: 'Bearer ' + localStorage.getItem("usuario-login"),
      },
    })
      .then((resposta) => {
        this.setState({ listaMedico: resposta.data });
        console.log(this.state.listaMedico);
      })

      .catch((erro) => console.log(erro));
  };

  atualizaDescricao = (consulta) => {
    this.setState(
      {
        idConsultaAlterada: consulta.idConsulta,
      },
      () => {
        console.log(
          "O Tipo de Evento " + consulta.idConsulta + " foi selecionado, ",

          "agora o valor do state idTipoEventoAlterado é: " +
            this.state.idConsultaAlterada,

          "e o valor do state titulo é: " + this.state.descricao
        );

        this.buscarConsultasMedico();
      }
    );
  };

  atualizaEstadoDescricao = async (event) => {
    await this.setState({ descricao: event.target.value });
    console.log(this.state.descricao);
  };

  atualizaBotaoDescricao = () => {
    axios.patch(
      "http://localhost:5000/api/Consulta/medico/" +
        this.state.idConsultaAlterada,
      { descricao: this.state.descricao }
    );
  };

  componentDidMount() {
    this.buscarConsultasMedico();
  }

  render() {
    return (
      <div>
        <main>
          <section>
            <h2>CONSULTAS de médicos</h2>
            <table style={{ borderCollapse: "separate", borderSpacing: 30 }}>
              <thead>
                <tr>
                  <th>Nome do médico</th>
                  <th>Especialidade</th>
                  <th>Data da consulta</th>
                  <th>Nome do paciente</th>
                  <th>Situação</th>
                  <th>Descrição do paciente</th>
                </tr>
              </thead>

              <tbody>
                {this.state.listaMedico.map((consulta) => {
                  return (
                    <tr key={consulta.idConsulta}>
                      <td>{consulta.idMedicoNavigation.nomeMedico}</td>
                      <td>
                        {
                          consulta.idMedicoNavigation.idEspecialidadeNavigation
                            .nomeEspecialidade
                        }
                      </td>
                      <td>
                        {Intl.DateTimeFormat("pt-BR").format(
                          new Date(consulta.dataConsulta)
                        )}
                      </td>
                      <td>{consulta.idPacienteNavigation.nomePaciente}</td>
                      <td>{consulta.idSituacaoNavigation.situacao1}</td>
                      <td>{consulta.descricao}</td>
                      <td>
                        <button
                          onClick={() => this.atualizaDescricao(consulta)}
                        >
                          Selecionar
                        </button>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>

            <form onSubmit={this.atualizaBotaoDescricao}>
              <input
                type="text"
                name="descricao"
                onChange={this.atualizaEstadoDescricao}
                placeholder="Edite sua descrição"
                value={this.state.descricao}
              />

              <button type="submit">Selecionar</button>
            </form>
          </section>
        </main>
      </div>
    );
  }
}
