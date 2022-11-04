<template>
    <v-container fluid fill-height class="log">
      <v-row align="center" justify="center" class="py-5">
        <v-col cols="12" sm="8" md="8">
          <v-card class="elevation-12">
          
                <v-row align="center" justify="center">
                  <v-col cols="12" md="10">
                    <v-card-text class="mt-12">
                      <h1 class="text-center siva">Kreiranje admina</h1>
                      <div class="text-center mt-4">
                       <i class="fa-solid h2 fw-bold fa-user-lock"></i>
                     </div>

                      <v-form @submit.prevent="handleSubmit" enctype="multipart/form-data">

                      <!-- ime -->
                     <v-text-field
                          label="Ime *"
                          name="firstName"
                          v-model="firstName"
                          type="text"
                          prepend-icon="fa-solid fa-signature"
                          color="amber darken-1"
                          @blur="$v.firstName.$touch()"                            
                        />
                        <template v-if="$v.firstName.$error">
                        <p class='text-danger' v-if="!$v.firstName.required">Ime je obavezno.</p>
                        <p class='text-danger' v-else-if="!$v.firstName.minLengthValue">Ime treba da ima najmanje 2 karaktera.</p>
                        <p class='text-danger' v-else-if="!$v.firstName.maxLengthValue">Ime sme da ima najviše 40 karaktera.</p>
                        <p class='text-danger' v-else-if="!$v.firstName.flReg">U imenu smete koristiti slova, specijalne karaktere i razmake (npr. Sierra O'Neil)</p>
                        </template>
                        <!-- prezime -->
                        <v-text-field
                          label="Prezime *"
                          name="lastName"
                          v-model="lastName"
                          type="text"
                          prepend-icon="fa-solid fa-signature"
                          color="amber darken-1"
                          @blur="$v.lastName.$touch()"                            
                        />
                        <template v-if="$v.lastName.$error">
                        <p class='text-danger' v-if="!$v.lastName.required">Prezime je obavezno.</p>
                        <p class='text-danger' v-else-if="!$v.lastName.minLengthValue">Prezime treba da ima najmanje 3 karaktera.</p>
                        <p class='text-danger' v-else-if="!$v.lastName.maxLengthValue">Prezime sme da ima najviše 40 karaktera.</p>
                        <p class='text-danger' v-else-if="!$v.lastName.flReg">U prezimenu smete koristiti slova, specijalne karaktere i razmake (npr. Sierra O'Neil)</p>

                        </template>
                        <!-- korisnicko ime -->
                      <v-text-field
                          label="Korisničko ime *"
                          name="username"
                          v-model="username"
                          type="text"
                          prepend-icon="fa-duotone fa-user-astronaut"
                          color="amber darken-1"
                          @blur="$v.username.$touch()"                            
                        />
                        <template v-if="$v.username.$error">
                        <p class='text-danger' v-if="!$v.username.required">Korisničko ime je obavezno.</p>
                        <p class='text-danger' v-else-if="!$v.username.minLengthValue">Korisničko ime treba da ima najmanje 4 karaktera.</p>
                        <p class='text-danger' v-else-if="!$v.username.maxLengthValue">Korisničko ime sme da ima najviše 25 karaktera.</p>
                        <p class='text-danger' v-else-if="!$v.username.uReg">Korisničko ime mora početi i završiti se slovom ili brojem i sme sadržati slova, brojeve, . i _</p>
                      </template>

                         <!-- email -->
                      <v-text-field
                          label="Email *"
                          name="email"
                          v-model="email"
                          type="text"
                          prepend-icon="mdi-at"
                          color="amber darken-1"
                          @blur="$v.email.$touch()"                            
                        />
                        <template v-if="$v.email.$error">
                        <p class='text-danger' v-if="!$v.email.required">E-mail je obavezan.</p>
                        <p class='text-danger' v-else-if="!$v.email.email">Neispravan format e-maila.</p>
                           <p class='text-danger' v-else-if="!$v.email.maxLengthValue">E-mail sme da ima najviše 100 karaktera.</p>

                        </template>
                           <!-- lozinka -->
                       <v-text-field
                          id="password"
                          label="Lozinka *"
                           prepend-icon="mdi-lock"
                          name="password"
                             v-model="password.password"
                         :type="passwordFieldType"
                         append-icon="fa-solid fa-eye"
                           @click:append="switchVisibility"
                          color="amber darken-1"
                           @blur="$v.password.password.$touch()"   
                        />
                        <template v-if="$v.password.password.$error">
                        <span class='text-danger' v-if="!$v.password.password.required">Lozinka je obavezna.</span>
                        <p class='text-danger' v-else-if="!$v.password.password.minLengthValue">Lozinka treba da ima najmanje 8 karaktera.</p>
                        <p class='text-danger' v-else-if="!$v.password.password.maxLengthValue">Lozinka sme da ima najviše 15 karaktera.</p>
                        <p class='text-danger' v-else-if="!$v.password.password.pRеg">Lozinka mora sadržati najmanje po jedno veliko i malo slovo, jedan broj i jedan specijalan karakter.</p>
                        </template>

                        

                         <!-- potvrdj. lozinka -->
                        <v-text-field
                          id="password_confirm"
                          label="Potvrdite lozinku *"
                           prepend-icon="mdi-checkbox-marked-circle"
                           append-icon="fa-solid fa-eye"
                           @click:append="switchVisibility"
                          name="password.confirm"
                          v-model="password.confirm"
                          :type="passwordFieldType"
                          color="amber darken-1"
                           @blur="$v.password.confirm.$touch()"   
                        />
               
                        <template v-if="$v.password.confirm.$error">
                        <p class='text-danger' v-if="!$v.password.confirm.required">Morate ponoviti lozinku.</p>
                        <p class='text-danger' v-else-if="!$v.password.confirm.sameAsPassword">Lozinke se ne poklapaju.</p>
                        </template>
                       <!-- slika-->
                        <template>
                          <v-file-input
                            :rules="rules"
                            accept=".jpg,.jpeg,.png,.gif"
                            show-size
                            counter
                            v-model="image"
                            @change="selectedImage"
                            label="Profilna fotografija"
                            prepend-icon="mdi-camera">
                          </v-file-input>
                        </template>
                       
                           <template v-if="feedback"> 
   
                          <div class="pt-5">
                             <p v-for="f in feedback" class="text-danger text-center fw-bold">   {{ f.error }}</p>
                           </div>
                           </template>
                         <div class="text-center mt-3"> 
                           <v-btn type="submit" rounded color="amber darken-1" class="my-2 m-1" :disabled="$v.$invalid">Kreiraj admina</v-btn>
                         
                           <v-btn to="/admin/users"  class="my-2"  rounded color="green text-white m-1 darken-1">SVI KORISNICI</v-btn>
                       </div>
                      </v-form>
                    </v-card-text>
                                             
                   
                  </v-col>
                 
                </v-row>
                          
        
            
          </v-card>
        </v-col>
      </v-row>
    </v-container>
</template>

<script>
import axios from 'axios';
import {required,email,sameAs,minLength,maxLength,helpers} from 'vuelidate/lib/validators'
const flReg=helpers.regex('flReg',/^[A-ZŠĐČĆŽa-zšđčćž]+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*$/);
const pReg=helpers.regex('preg',/^(?=.*[a-zšđčćž])(?=.*[A-ZŠĐČĆŽ])(?=.*\d)(?=.*[@$!%*?&])[A-ZŠĐČĆŽa-zšđčžć\d@$!%*?&]{8,15}$/);
const uReg=helpers.regex('ureg',/^(?=.{4,30}$)(?![_.])(?!.*[_.]{2})[a-zšđčćžA-ZŠĐČĆŽ0-9._]+(?<![_.])$/)

export default {
data: () => ({
  rules: [
        image =>   !image || image.size < 2e6 || 'Veličina slike mora da bude manja od 2 MB!',
      
        image =>  !image || (/\.(gif|GIF|jpe?g|JPE?G|png|PNG)$/i).test(image.name) || 'Dozvoljene ekstenzije su .jpg, .jpeg, .png, .gif'
    ],
    firstName:'',
    lastName:'',
    username:'',
    email:'',
    password:{
      password:'',
      confirm:''
    },
    passwordFieldType: "password",
    RoleId:1,
    image:null,
    feedback:[]
}),
validations: {
    firstName:{required, minLengthValue: minLength(2), maxLengthValue:maxLength(40),flReg:flReg},   
    lastName:{required, minLengthValue: minLength(3),maxLengthValue:maxLength(40),flReg:flReg},
    username:{required,minLengthValue: minLength(4),maxLengthValue:maxLength(25),uReg:uReg},
   email:{required,email,maxLengthValue:maxLength(100)},
   password:{
      password:{required, minLengthValue: minLength(8),maxLengthValue:maxLength(15),preg:pReg},
      confirm:{required,sameAsPassword: sameAs('password')}
   }
},

 methods: {


      switchVisibility() {
           this.passwordFieldType = this.passwordFieldType === "password" ? "text" : "password";
     },
      selectedImage(e){
        this.image=e
   //     console.log(e)
      },

     handleSubmit(){
      this.$v.$touch()
      if(!this.$v.$invalid){
       // console.log('sve ok')
        let formData = new FormData();
        formData.append('FirstName', this.firstName);
        formData.append('LastName', this.lastName);
        formData.append('Username', this.username);
        formData.append('Email', this.email);
        formData.append('Password', this.password.confirm);
        formData.append('RoleId', this.RoleId);
        if(this.image){
          formData.append('Image', this.image,this.image.name);
        }
        else{
          formData.append('Image', null);

        }

          axios.post('users',formData)
          .then(()=>{
            this.$router.replace({
            name:'users'
             })
          })
          .catch(err=>{
            this.feedback=err.response.data.errors;
           // console.log(this.feedback)
          })
        
        }
        
     }

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


</style>