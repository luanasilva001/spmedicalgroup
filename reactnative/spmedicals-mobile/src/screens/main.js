import React, { Component } from "react";
import { Image, StyleSheet, View } from "react-native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";

import Consultas from "./consultas";
import Perfil from "./perfil";

const bottomTab = createBottomTabNavigator();

export default class Main extends Component {
  render() {
    return (
      <View style={styles.container}>
        <bottomTab.Navigator
          initialRouteName="Login"
          tabBarOptions={{
            showLabel: false,
            showIcon: true,
            activeBackgroundColor: "#2D6157",
            inactiveBackgroundColor: "#A8EDBA",
            activeTintColor: "#fff",
            inactiveTintColor: "#fff",
            style: { height: 50 },
          }}
          screenOptions={({ route }) => ({
            tabBarIcon: () => {
              if (route.name === "Consultas") {
                return (
                  <Image
                    source={require("../../assets/imgs/prancheta.png")}
                    style={styles.tabBarIcon}
                  />
                );
              }
              if (route.name === "Perfil") {
                return (
                  <Image
                    source={require("../../assets/imgs/user.png")}
                    style={styles.tabBarIcon}
                  />
                );
              }
            },
          })}
        >
          <bottomTab.Screen name="Consultas" component={Consultas} />
          <bottomTab.Screen name="Perfil" component={Perfil} />
        </bottomTab.Navigator>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#F1F1F1",
  },

  tabBarIcon: {
    width: 45,
    height: 45,
  },
});
