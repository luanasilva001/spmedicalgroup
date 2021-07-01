import React, { Component } from "react";
import { FlatList, Image, StyleSheet, Text, View } from "react-native";
import AsyncStorage from "@react-native-async-storage/async-storage";
import api from "../services/api";
import jwtDecode from "jwt-decode";

export default class Medicos extends Component {
  constructor(props) {
    super(props);
    this.state = {
      listaConsultas: [],
    };
  }

  buscarConsultas = async () => {
    const valorToken = await AsyncStorage.getItem("usuario-login");

    let URL = "/consulta/";

    if (jwtDecode(valorToken).role === "2") {
      URL = "/consulta/medico-consulta";

    }else if(jwtDecode(valorToken).role === "3") {
      URL = "/consulta/paciente-consulta";
    }

    const resposta = await api.get(URL, {
      headers: {
        'Authorization': 'Bearer ' + valorToken
      },
    });

    const dadosDaApi = resposta.data;

    this.setState({ listaConsultas: dadosDaApi });
  };

  // buscarPacientes = async () => {
  //   const resposta = await api.get('/consulta/paciente-consulta', {
  //     headers: {
  //       'Authorization' : 'Bearer ' + valorToken
  //     }
  //   })
  //   const dadosDaApi = resposta.data;
  //   this.setState({listaPacientes : dadosDaApi})
  // }

  // buscarMedicos = async () => {
  //   const valorToken = await AsyncStorage.getItem('usuario-login')
  //   const resposta = await api.get('/consulta/medico-consulta', {
  //     headers: {
  //       'Authorization' : 'Bearer ' + valorToken
  //     }
  //   })
  //   const dadosDaApi = resposta.data;
  //   this.setState({listaMedicos : dadosDaApi})
  // }

  componentDidMount() {
    // this.buscarMedicos();
    // this.buscarPacientes();
    this.buscarConsultas();
  }

  render() {
    return (
      <View style={styles.container}>
        {/* header */}
        <View style={styles.mainHeader}>
          <View style={styles.mainHeaderRow}>
            <Image
              source={require("../../assets/imgs/estetoscopio.png")}
              style={styles.mainHeaderImg}
            />
            <Text style={styles.mainHeaderText}>
              {"Consultas".toUpperCase()}
            </Text>
          </View>
          <View style={styles.mainHeaderLine} />
          {/* fim header */}

          {/* corpo */}
          <View style={styles.mainBody}>
            <FlatList
              contentContainerStyle={styles.mainBodyContent}
              data={this.state.listaConsultas}
              keyExtractor={(item) => item.descricao}
              renderItem={this.renderItem}
            />
          </View>
          {/* fim corpo */}
        </View>
      </View>
    );
  }

  renderItem = ({ item }) => (
    <View style={styles.flatItemRow}>
      <View style={styles.flatItemContainer}>
        <Text style={styles.flatItemTitle}>
          Nome do paciente: {item.idPacienteNavigation.nomePaciente}
        </Text>
        <Text style={styles.flatItemInfo}>Especialidade do médico: {item.idMedicoNavigation.idEspecialidadeNavigation.nomeEspecialidade}</Text>
        <Text style={styles.flatItemInfo}>
          Data da Consulta:{" "}
          {Intl.DateTimeFormat("pt-BR").format(new Date(item.dataConsulta))}
        </Text>
        <Text style={styles.flatItemInfo}>Nome do médico: {item.idMedicoNavigation.nomeMedico}</Text>
        <Text style={styles.flatItemInfo}>
          Situação da Consulta: {item.idSituacaoNavigation.situacao1}
        </Text>
        <Text style={styles.flatItemInfo}>
          Descrição do paciente: {item.descricao}
        </Text>
      </View>
      <View style={styles.flatItemRow}>
        {/* <Image 
     source={require('./assets/imgs/estetoscopio.png')}
     style={styles.flatItemImgIcon}
     /> */}
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#F1F1F1",
  },

  flatItemRow: {
    color: "red",
  },

  mainHeader: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    paddingTop: 20,
  },

  mainHeaderRow: {
    flexDirection: "row",
  },

  mainHeaderImg: {
    width: 35,
    height: 35,
    tintColor: "#999",
    marginRight: -8,
    marginTop: -12,
    zIndex: -1,
  },

  mainHeaderText: {
    fontSize: 16,
    letterSpacing: 5,
    color: "#999",
  },

  mainHeaderLine: {
    width: 220,
    paddingTop: 10,
    borderBottomColor: "#047D69",
    borderBottomWidth: 1,
  },

  mainBody: {
    flex: 4,
  },
  mainBodyContent: {
    paddingTop: 30,
    paddingRight: 50,
    paddingLeft: 50,
  },

  flatItemRow: {
    flexDirection: "column",
    borderBottomWidth: 1,
    borderBottomColor: "#047D69",
    marginTop: 30,
  },

  flatItemContainer: {
    flex: 1,
  },

  flatItemTitle: {
    color: "#333",
    fontWeight: "bold",
    textTransform: "uppercase",
    fontSize: 16,
  },

  flatItemInfo: {
    fontSize: 16,
    color: "#999",
    lineHeight: 20,
  },
});
