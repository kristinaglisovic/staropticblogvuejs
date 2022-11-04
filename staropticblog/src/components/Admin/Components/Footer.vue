<template>
  <v-footer
    dark
    padless
  >
  
  <v-card
      class="flex"
      flat
      tile
    >
       
        

        
        

      <v-card-text class="py-4 my-3 white--text text-center">
        <v-icon size="22px">mdi-copyright </v-icon> <strong>{{ new Date().getFullYear() }}  —</strong> <v-icon size="22px">mdi-star-outline</v-icon><strong>  Star Optik</strong>
      </v-card-text>
     
    </v-card>
  </v-footer>
</template>

<style scoped>

.siva{
  background-color: #7C99AC !important;
}
.senka{
   text-shadow: 1px 2px 3px #666;
}
</style>
<script>
  export default {
    data: () => ({
      icons: [
      {
        i:'mdi-facebook',
        color:'fb'
      },
         {
        i:'mdi-gmail',
        color:'gmail'
      },
      {
        i:'mdi-instagram',
        color:'instagram'
      },
      ],
    }),
  }
</script>