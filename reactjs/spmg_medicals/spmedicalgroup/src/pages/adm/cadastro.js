import axios from 'axios';
import { Component } from 'react'

class Cadastro extends Component {
  constructor(props) {
    super(props);
    this.state = {
      idConsulta: 0,
      idMedico: 0,
      listaMedico: [],
      idPaciente: 0,
      listaPaciente: [],
      situacao: 0,
      listaSituacoes: [],
      dataConsulta: (new Date()).getFullYear(),
      descricao: '',
      isLoading: false,
    }
  };

  buscarMedicos = () => {
    axios('http://localhost:5000/api/medico', {
      headers : {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

      .then(resposta => {
        if (resposta.status === 200) {
          this.setState({ listaMedico: resposta.data })
          console.log(this.state.listaMedico)
        }
      })

      .catch(erro => console.log(erro))
  }

  buscarPacientes = () => {
    axios('http://localhost:5000/api/paciente', {
      headers: {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

      .then(resposta => {
        if (resposta.status === 200) {
          this.setState({ listaPaciente: resposta.data })
          console.log(this.state.listaPaciente)
        }
      })
      .catch(erro => console.log(erro))
  }

  buscarSituacoes = () => {
    axios('http://localhost:5000/api/situacao', {
      headers : {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

      .then(resposta => {
        if (resposta.status === 200) {
          this.setState({ listaSituacoes: resposta.data })
          console.log(this.state.listaSituacoes)
        }
      })

      .catch(erro => console.log(erro))
  }

  componentDidMount() {
    this.buscarMedicos();
    this.buscarPacientes();
    this.buscarSituacoes();
  };

  cadastrarConsulta = (event) => {
    event.preventDefault();
    this.setState({ isLoading: true })

    let consulta = {
      idMedico: this.state.idMedico,
      idPaciente: this.state.idPaciente,
      idSituacao: this.state.situacao,
      dataConsulta: this.state.dataConsulta,
      descricao: ''
    }

    console.log(consulta);
    console.log(localStorage.getItem('usuario-login'))

    axios.post('http://localhost:5000/api/Consulta', consulta, {
      headers : {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

      .then(resposta => {
        if (resposta.status === 201) {
          console.log('Consulta cadastrada!')
          this.setState({ isLoading: false })
        }
      })
      .catch(erro => {
        console.log(erro);
        this.setState({ isLoading: false })
      })
  }

  atualizaStateCampo = async (campo) => {
    await this.setState({ [campo.target.name]: campo.target.value })
    console.log(this.state.situacao)
  }


  render() {
    return (
      <main>
        <section>
          <h2>Cadastroooo</h2>
          <form onSubmit={this.cadastrarConsulta}>
            <div style={{ display: 'flex', flexDirection: 'column', width: '20vw' }}>

              <select
                name="situacao"
                value={this.state.situacao}
                onChange={this.atualizaStateCampo}
              >

                <option>Selecione a situação a ser realizada</option>
                <option value="1">Realizada</option>
                <option value="2">Cancelada</option>
                <option value="3">Agendada</option>
              </select>

              <select
                name="idMedico"
                value={this.state.idMedico}
                onChange={this.atualizaStateCampo}
              >
                <option >Medicos</option>
                {
                  this.state.listaMedico.map(consultas => {
                    return (
                      <option key={consultas.idMedico} value={consultas.idMedico}>
                        {consultas.nomeMedico}
                      </option>
                    );
                  })
                }
              </select>

              <select
                name="idPaciente"
                value={this.state.idPaciente}
                onChange={this.atualizaStateCampo}
              >
                <option >Pacientes</option>
                {
                  this.state.listaPaciente.map(consultas => {
                    return (
                      <option key={consultas.idPaciente} value={consultas.idPaciente}>
                        {consultas.nomePaciente}
                      </option>
                    );
                  })
                }
              </select>

              <input
                type="descricao"
                name="descricao"
                value={this.state.descricao}
                onChange={this.atualizaStateCampo}
              />

              <input
                type="date"
                name="dataConsulta"
                value={this.state.dataConsulta}
                onChange={this.atualizaStateCampo}
              />

              {
                // Caso seja true, renderiza um botão desabilitado com o texto 'Loading...'
                this.state.isLoading === true &&
                <button type="submit" disabled>
                  Loading...
              </button>
              }

              {
                // Caso seja false, renderiza um botão habilitado com o texto 'Cadastrar'
                this.state.isLoading === false &&
                <button type="submit">
                  Cadastrar
               </button>
              }

            </div>
          </form>
        </section>
      </main >
    )
  }
}

export default Cadastro;

