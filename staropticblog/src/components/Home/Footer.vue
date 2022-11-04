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
       
      <v-card-title class="siva">
        <strong class="senka center">BUDIMO U KONTAKTU I PREKO <br class='br d-none'>DRUŠTVENIH MREŽA!</strong>
 <v-spacer></v-spacer>

        <div class="center padd">
<v-btn
          v-for="icon in icons" :key="icon.id"
          class="mx-4"
          dark
          icon
        >
          <v-icon size="24px" class="p-3 shadow rounded-circle bg-white" :class="icon.color">
            {{ icon.i }}
          </v-icon>
        </v-btn>
        </div>
        
      </v-card-title>

      <v-card-text class="py-2 white--text text-center">
        <v-icon size="22px">mdi-copyright </v-icon> <strong>{{ new Date().getFullYear() }}  —</strong> <v-icon size="22px">mdi-star-outline</v-icon><strong>  Star Optik</strong>
      </v-card-text>
    </v-card>
  </v-footer>
</template>

<style scoped>

@media only screen and (max-width: 745px) {

    
    .center{
      text-align: center;
      width: 100%;
    }
    .none{
      display: none !important;
    }

    .padd{
      padding: 7px 0px;
    }
}

@media only screen and (max-width: 529px) {
    .br{
      display: block !important;
    }
   
}

.fb{
  color: #003091 !important;

}
.gmail{
  color: #ff0800 !important;

}
.instagram{
  color: #c13584 !important;
}

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