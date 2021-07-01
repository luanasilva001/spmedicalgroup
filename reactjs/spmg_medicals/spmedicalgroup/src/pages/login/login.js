import React, { Component } from "react";
import axios from "axios";

import "../../pages/style.css";

import { parseJwt, usuarioAutenticado } from "../../services/auth";

import imagem_fundo from "../imgs/index/imagem_fundo.png";

class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: "",
      senha: "",
      erroMensagem: "",
      isLoading: false,
    };
  }

  atualizaStateCampo = (campo) => {
    this.setState({ [campo.target.name]: campo.target.value });
  };

  efetuaLogin = (event) => {
    event.preventDefault();

    this.setState({ erroMensagem: "", isLoading: true });

    axios
      .post("http://localhost:5000/api/Login", {
        email: this.state.email,
        senha: this.state.senha,
      })

      .then((resposta) => {
        if (resposta.status === 200) {
          localStorage.setItem("usuario-login", resposta.data.token);

          // console.log('meu token é: ' + resposta.data.token)
          this.setState({ isLoading: false });

          console.log(parseJwt().role);

          switch (parseJwt().role) {
            case "1":
              this.props.history.push("/consultas");
              break;
            case "2":
              this.props.history.push("/medico");
              break;
            case "3":
              this.props.history.push("/paciente");
              break;
            default:
              this.props.history.push("/");
              break;
          }
        }
      })

      .catch(() => {
        this.setState({
          erroMensagem: "E-mail ou senha incorretos!",
          isLoading: false,
        });
      });
  };

  render() {
    return (
      <div>
        <main>
          <section>
            <div className="login">
              <div className="login-img">
                <img src={imagem_fundo} alt="Imagem de fundo" />
              </div>

              <div className="login-formulario">
                <h1 className="login-titulo">Login</h1>

                <form onSubmit={this.efetuaLogin}>
                  <div className="email">
                    <label className="email-paragrafo">e-mail</label>

                    <input
                      className="email-input"
                      type="text"
                      name="email"
                      value={this.state.email}
                      onChange={this.atualizaStateCampo}
                      placeholder="email@email.com"
                    />
                  </div>

                  <div className="senha">
                    <label className="senha-paragrafo">senha</label>

                    <input
                      className="senha-input"
                      type="password"
                      name="senha"
                      value={this.state.senha}
                      onChange={this.atualizaStateCampo}
                      placeholder="Digite a sua senha"
                    />
                  </div>

                  <div className="lembrar-de-mim-checkbox">
                    <input type="checkbox" className="checkbox" />
                    <label className="lembrar-paragrafo">lembrar de mim</label>
                  </div>

                  <br/>
                  
                  <p style={{ color: "red" }}>{this.state.erroMensagem}</p>

                  {this.state.isLoading === true && (
                    <div className="btn">
                    <button type="submit" className="btn-logar" disabled>
                      Loading
                    </button>
                    </div>
                  )}
                  {this.state.isLoading === false && (
                    <div className="btn">
                    <button
                      className="btn-logar"
                      type="submit"
                      disabled={
                        this.state.email === "" || this.state.section === ""
                          ? "none"
                          : ""
                      }
                    >
                      Logar
                    </button>
                  </div>
                  )}

                  {/* <button type="submit">Logar</button> */}
                </form>
              </div>
            </div>
          </section>
        </main>
      </div>
    );
  }
}

export default Login;
