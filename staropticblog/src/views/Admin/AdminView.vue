<template>
  <v-app id="inspire">
    <Navigation></Navigation>
    <v-main class="p-0">
      <v-container class="py-8 px-6" fluid>
          <router-view>
            
          </router-view>
      </v-container>
      </v-main>
    <Footer></Footer>
  </v-app>
</template>

<script>
import Navigation from "../../components/Admin/Components/Navigation.vue";
import Footer from "../../components/Admin/Components/Footer.vue";
export default {
  name: "Admin",
  components: { Navigation,Footer},
  data: () => ({
    cards: ["Today", "Yesterday"],
    drawer: null,
  }),
  methods: {},
};
</script>
<style scoped>
#inspire{
  background-image: url(../../../public/img/bg.jpg);
  background-size:cover;
}
</style>
