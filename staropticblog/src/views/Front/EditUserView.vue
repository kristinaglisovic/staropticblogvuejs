<template>
    <v-container fluid fill-height class="log" v-if="mounted">
      <v-row align="center" justify="center" class="py-5">
        <v-col cols="12" sm="8" md="8">
          <v-card class="elevation-12">
          
                <v-row align="center" justify="center">
                  <v-col cols="12" md="10">
                    <v-card-text class="mt-12">
                      <h4 class="text-center">{{user.firstName}} {{user.lastName}}</h4>
                      <div class="text-center mt-4">
                        <h3><i class="fa-solid fa-user-pen"></i></h3>
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
                 
                       
                           <div class="d-flex align-items-center justify-content-around flex-wrap py-3">
                               <template v-if="user.image!='No image'"> 
                                <h2>Profilna slika:</h2>

                                    <v-img    max-height="150"
                                    max-width="220" v-bind:src="'http://localhost:5000/Images/'+user.image"></v-img>
                                </template>
                                <template v-else>
                                    <h3>Nema profilnu sliku</h3>
                                    
                                </template>
                                    
                            </div>



                       <!-- slika-->
                        <template v-if="user.image=='No image'">
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
                        <template v-else>
                            <div class="text-center pt-5">

                              <h6 class="pb-3">Ako želite da promenite sliku, prvo obrišite trenutnu</h6>
                              <v-btn @click='deletePhoto(user.imageId)' class="bg-danger text-white ikona"><i class="fa-solid fa-trash-can"></i></v-btn>
                            </div>

                        </template>
                       
                          
                         <div class="text-center mt-3"> 
                           <v-btn type="submit" rounded color="amber darken-1" :disabled="$v.$invalid">Ažuriraj</v-btn>
                         
                         </div>
                         
                      </v-form>
                    </v-card-text>
                         <template v-if="feedback!=''">
                                
                                <p class="text-danger text-center fw-bold">{{feedback}}</p>
                                                
                          </template>

                          <template v-if="feedback2!='' && feedback==''">
                                
                                <p class="text-success text-center fw-bold">{{feedback2}}</p>
                                                
                          </template>
                     
                   
                  </v-col>
                 
                </v-row>
                          
        
            
          </v-card>
        </v-col>
      </v-row>
    </v-container>
</template>

<script>
import { mapGetters,mapActions } from 'vuex';
import axios from 'axios';
import {required,email,minLength,maxLength,helpers} from 'vuelidate/lib/validators'
const flReg=helpers.regex('flReg',/^[A-ZŠĐČĆŽa-zšđčćž]+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*$/);
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
     
      mounted:false,
    roles:[
      {
        name:"Administrator",
        id:"1"
      },
      {
        name:"Korisnik",
        id:"2"
      }
    ],
    user:null,
    RoleId:null,
    image:null,
    feedback:'',
    feedback2:''
}),
validations: {
    firstName:{required, minLengthValue: minLength(2), maxLengthValue:maxLength(40),flReg:flReg},   
    lastName:{required, minLengthValue: minLength(3),maxLengthValue:maxLength(40),flReg:flReg},
    username:{required,minLengthValue: minLength(4),maxLengthValue:maxLength(25),uReg:uReg},
   email:{required,email,maxLengthValue:maxLength(100)},
 
},

 methods: {

    resetF(){
      this.feedback2=''
    },


    loadUser(){
        
        axios.get(`users/${this.user2.id}`)
             .then(response => {
                console.log(response.data);
                this.user=response.data;
                this.firstName=response.data.firstName;
                this.lastName=response.data.lastName;
                this.email=response.data.email;
                this.username=response.data.username;
                this.RoleId=response.data.roleId;
                if(this.feedback2){
                  setTimeout(this.resetF, 1400)
                }
                this.mounted=true;
             
            })
             .catch(error => {
                if (error.response) {
                  //response status is an error code
              //console.log(error.response.status);
                if(error.response.status==404){
                    this.$router.push('/admin/users')
                }

               }
             else if (error.request) {
              //response not received though the request was sent
                  console.log(error.request);
              }
               else {
                      //an error occurred when setting up the request
              console.log(error.message);
          }
       })
    },

    setRole(id){
      this.RoleId=id;
    },

    deletePhoto(id){
                  //console.log(id)

               axios.delete('images/'+id)
                    .then(()=>{
                     
                       this.loadUser()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             },


      
      selectedImage(e){
        this.image=e
        console.log(e)
      },

     handleSubmit(){
      this.$v.$touch()
      if(!this.$v.$invalid){
       // console.log('sve ok')
        let formData = new FormData();
        formData.append('UserId', this.user2.id);
        formData.append('FirstName', this.firstName);
        formData.append('LastName', this.lastName);
        formData.append('Username', this.username);
        formData.append('Email', this.email);
        formData.append('RoleId', 0);
        if(this.image){
          formData.append('Image', this.image,this.image.name);
        }
        else{
          formData.append('Image', null);
        }

          axios.patch('users/edit',formData)
          .then(()=>{
            this.feedback=''
            this.feedback2='Korisnik je uspešno ažuriran.'
            this.loadUser()
          })
          .catch(err=>{
            console.log(err)
            this.feedback=err.response.data.message;
           // console.log(this.feedback)
          })
        
        }
        
     }

 },
 mounted(){
    this.loadUser();
 },
 computed:{
      ...mapGetters({
         authenticated:'auth/authenticated',
         user2:'auth/user'
      })
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