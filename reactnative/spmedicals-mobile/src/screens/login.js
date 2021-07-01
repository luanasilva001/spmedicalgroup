import React, { Component } from "react";
import {
  Image,
  StyleSheet,
  Text,
  TextInput,
  View,
  TouchableOpacity,
} from "react-native";
import AsyncStorage from "@react-native-async-storage/async-storage";
import api from "../services/api";

export default class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: "",
      senha: "",
    };
  }

  realizarLogin = async () => {
    console.warn(this.state.email + " " + this.state.senha);

    const resposta = await api.post("/login", {
      email: this.state.email,
      senha: this.state.senha,
    });

    const token = resposta.data.token;

    await AsyncStorage.setItem("usuario-login", token);

    console.warn(token);

    this.props.navigation.navigate("Main");
  };

  render() {
    return (
      <View style={styles.container}>
        <Image
          source={require("../../assets/imgs/estetoscopio.png")}
          style={styles.mainImgLogin}
        />

        <TextInput
          style={styles.inputLogin}
          placeholder="email@email.com"
          placeholderTextColor="#047D69"
          keyboardType="email-address"
          onChangeText={(email) => this.setState({ email })}
        />

        <TextInput
          style={styles.inputLogin}
          placeholder="senha123"
          placeholderTextColor="#047D69"
          secureTextEntry={true}
          onChangeText={(senha) => this.setState({ senha })}
        />

        <TouchableOpacity style={styles.btnLogin} onPress={this.realizarLogin}>
          <Text style={styles.btnLoginText}>Login</Text>
        </TouchableOpacity>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#F1F1F1",
    justifyContent: "center",
    alignItems: "center",
  },

  mainImgLogin: {
    tintColor: "#F1F1F1",
    height: 100,
    width: 100,
    borderRadius: 50,
    backgroundColor: "#047D69",
    margin: 60,
    marginTop: 0,
  },

  inputLogin: {
    width: 240,
    marginBottom: 40,
    fontSize: 18,
    color: "#047D69",
    borderBottomColor: "#047D69",
    borderBottomWidth: 2,
    borderTopColor: "#047D69",
    borderTopWidth: 2,
    borderLeftColor: "#047D69",
    borderLeftWidth: 2,
    borderRightColor: "#047D69",
    borderRightWidth: 2,
  },

  btnLogin: {
    alignItems: "center",
    justifyContent: "center",
    height: 38,
    width: 240,
    backgroundColor: "#FFF",
    borderColor: "#FFF",
    borderWidth: 1,
    borderRadius: 4,
    shadowOffset: { height: 1, width: 1 },
    marginTop: 20,
  },

  btnLoginText: {
    fontSize: 18,
    fontWeight: "bold",
    color: "#047D69",
    letterSpacing: 6,
    textTransform: "uppercase",
  },
});
