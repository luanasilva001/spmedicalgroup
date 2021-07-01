import axios from 'axios'
import { Component } from 'react'

export default class ConsultasPaciente extends Component {
  constructor(props) {
    super(props);
    this.state = {
      listaPaciente: []
    }
  };

  buscarConsultasPaciente = () => {
    axios('http://localhost:5000/api/Consulta/paciente-consulta', {
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

      .then(resposta => {
        this.setState({ listaPaciente: resposta.data })
        console.log(this.state.listaPaciente)
      })

      .catch(erro => console.log(erro))
  }

  componentDidMount() {
    this.buscarConsultasPaciente();
  }

  render() {
    return (
      <div>
        <main>
          <section>
            <h2>lista de consultas do paciente</h2>
            <table style={{ borderCollapse: "separate", borderSpacing: 30 }}>
              <thead>
                <tr>
                  <th>Nome do médico</th>
                  <th>Especialidade</th>
                  <th>Data da consulta</th>
                  <th>Nome do paciente</th>
                  <th>Situação</th>
                  <th>Minha descrição</th>
                </tr>
              </thead>


              <tbody>
                {
                  this.state.listaPaciente.map((consulta) => {
                    return (
                      <tr key={consulta.idConsulta}>
                        <td>{consulta.idMedicoNavigation.nomeMedico}</td>
                        <td>{consulta.idMedicoNavigation.idEspecialidadeNavigation.nomeEspecialidade}</td>
                        <td>{Intl.DateTimeFormat("pt-BR").format(new Date(consulta.dataConsulta))}</td>
                        <td>{consulta.idPacienteNavigation.nomePaciente}</td>
                        <td>{consulta.idSituacaoNavigation.situacao1}</td>
                        <td>{consulta.descricao}</td>
                      </tr>
                    )
                  })
                }
              </tbody>
            </table>
          </section>
        </main>
      </div>
    )
  }
}