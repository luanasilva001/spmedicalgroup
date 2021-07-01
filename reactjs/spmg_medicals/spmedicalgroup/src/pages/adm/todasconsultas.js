import axios from 'axios';
import { Component } from 'react';

class Consultas extends Component {
  constructor(props) {
    super(props);
    this.state = {
      listaConsultas: []
    }
  }

  buscarConsultas = () => {
    axios('http://localhost:5000/api/consulta/lista', {
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

      .then(resposta => {
        if (resposta.status === 200) {
          this.setState({ listaConsultas: resposta.data })
          console.log(this.state.listaConsultas)
        }
      })

      .catch(erro => console.log(erro))
  }


  componentDidMount() {
    this.buscarConsultas();
  }

  render() {
    return (
      <div>
        <main>
          <section>
            <h2>LISTA DE CONSULTAS</h2>
            <table style={{ borderCollapse: "separate", borderSpacing: 30 }}>
              <thead>
                <tr>
                  {/* <th>#</th> */}
                  <th>Nome paciente</th>
                  <th>Data da consulta</th>
                  <th>Nome médico</th>
                  <th>especialidade</th>
                  <th>Descrição</th>
                  <th>Situação</th>
                </tr>
              </thead>

              <tbody>
                {
                  this.state.listaConsultas.map((consultas) => {
                    return (
                      <tr key={consultas.idConsulta}>
                        {/* <td>{consultas.idConsulta}</td> */}
                        <td>{consultas.idPacienteNavigation.nomePaciente}</td>
                        <td>{Intl.DateTimeFormat("pt-BR").format(new Date(consultas.dataConsulta))}</td>
                        <td>{consultas.idMedicoNavigation.idEspecialidadeNavigation.nomeEspecialidade}</td>
                        <td>{consultas.descricao}</td>
                        <td>{consultas.idSituacaoNavigation.situacao1}</td>
                        <td>
                        </td>
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

export default Consultas;