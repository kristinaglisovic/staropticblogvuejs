<template>
      <v-container fluid fill-height class="log">
        <v-row align="center" justify="center" class="py-5">
          <v-col cols="12" sm="8" md="8">
            <v-card class="elevation-12">
            
                  <v-row align="center" justify="center">
                    <v-col cols="12" md="10">
                      <v-card-text class="mt-12">
                        <h1
                          class="text-center display-1 siva"
                        >Prijavite se na Star Optik Blog</h1>
                        <div class="text-center mt-4">
                          <i class="fa-solid h2 fw-bold fa-user-circle"></i>
                        </div>
                        <v-form @submit.prevent="handleSubmit">
                          <v-text-field
                            label="Email"
                            name="email"
                            v-model="email"
                            type="text"
                            prepend-icon="mdi-at"
                            color="amber darken-1"
                            @blur="$v.email.$touch()"                            
                          />
                          <template v-if="$v.email.$error">
                          <p class='text-danger' v-if="!$v.email.required">E-mail je obavezan.</p>
                          <p class='text-danger' v-if="!$v.email.email">Neispravan format e-maila.</p>
                          </template>

                          <v-text-field
                            id="password"
                            label="Lozinka"
                             prepend-icon="mdi-lock"
                            name="password"
                               v-model="password"
                        :type="passwordFieldType"
                            color="amber darken-1"
                             append-icon="fa-solid fa-eye"
                             @click:append="switchVisibility"
                             @blur="$v.password.$touch()"   
                          />
                          <template v-if="$v.password.$error">
                          <span class='text-danger' v-if="!$v.password.required">Lozinka je obavezna.</span>
                          </template>
                             <template v-if="feedback!=''">
                             <div class="pt-5">
                               <p class="text-danger text-center fw-bold">{{feedback}}</p>
                             </div>
                             </template>
                           <div class="text-center mt-3">
                             <v-btn type='submit' rounded color="amber darken-1" :disabled="$v.$invalid">Prijavi se</v-btn>
                         </div>
                        </v-form>
                      </v-card-text>
                        <p class="text-center mt-4 text-muted">Nemate nalog?<i class="fa-solid ml-2 fa-hand-point-right fw-bold text-muted"></i><v-btn text small class="ml-1 text-primary" to="/register">Registrujte se</v-btn></p>                        
                     
                    </v-col>
                   
                  </v-row>
          
              
            </v-card>
          </v-col>
        </v-row>
      </v-container>
  </template>

<script>
import { mapActions } from 'vuex';
import {required,email} from 'vuelidate/lib/validators'
export default {
  data: () => ({
      email:'',
      password:'',
      feedback:'',
      passwordFieldType: "password",
  }),
  validations: {
     email:{required,email},
     password:{required}
  },
  
   methods: {
      ...mapActions({
          signIn:'auth/signIn'
        
      }),

       handleSubmit(){
        this.$v.$touch()
        if(!this.$v.$invalid){
         this.signIn({
          email:this.email,
          password:this.password
        }).then(()=>{
          this.$router.replace({
            name:'home'
          }).catch(()=>{})
        }).catch((e)=>{
          
          //console.log(e.response.status)
          if(e.response.status==401){
                 this.feedback='Ne postoji korisnik sa unetim e-mailom i lozinkom.'
          }

            if(e.response.status==500){
                 this.feedback='Trenutno postoji problem sa serverom, molimo pokušajte kasnije.'
          }
        })
        }
        },
          switchVisibility() {
             this.passwordFieldType = this.passwordFieldType === "password" ? "text" : "password";
       },

   }
};
</script>

<style scoped>
a{
  text-decoration: none;
}

.siva{
    color:#7C99AC;
  }

.log{
  background-image: url(../../../public/img/auth.jpg);
  background-size: cover;
  background-repeat: no-repeat;
}

</style>