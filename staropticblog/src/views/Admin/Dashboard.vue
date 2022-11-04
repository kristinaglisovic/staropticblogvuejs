<template>
    <div class="dashboard" v-if="mounted">
        <v-subheader class="py-0 d-flex justify-space-between rounded-lg">
            <h3>Kontrolna tabla</h3>
            <span class="fw-bold" v-if="user">Admin: {{user.firstName}} {{user.lastName}}</span>
        </v-subheader>
        <br>
        <v-row>
            <v-col lg="7" cols="12">
                <v-row>
                    <v-col lg="6" cols="12">
                        <v-card elevation="10" class="rounded-lg" link to="/admin/users">
                            <v-card-text class="d-flex justify-space-between align-center">
                                <div>
                                    <strong>Korisnici</strong> <br>
                                    <span>Ukupan broj aktivnih korisnika</span>
                                </div>
                                <v-avatar size="60" style="border: 3px solid #444; background-color: navy;">
                                    <span class="fw-bold" style="color: white;"><i class='fa-solid fa-user'></i> {{users}}</span>
                                </v-avatar>
                            </v-card-text>
                            <v-card-actions class="d-flex justify-space-between">


                            </v-card-actions>
                        </v-card>
                    </v-col>
                    <v-col lg="6" cols="12">
                        <v-card elevation="10" class="rounded-lg" to="/admin/categories">
                            <v-card-text class="d-flex justify-space-between align-center">
                                <div>
                                    <strong>Kategorije</strong> <br>
                                    <span>Ukupan broj aktivnih kategorija</span>
                                </div>
                                <v-avatar size="60" class='bg-warning' style="border: 3px solid #444">
                                    <span class="fw-bold" style="color: navy"><i class="fa-brands fa-elementor"></i> {{cat}}</span>
                                </v-avatar>
                            </v-card-text>
                            <v-card-actions class="d-flex justify-space-between">


                            </v-card-actions>
                        </v-card>
                    </v-col>
                    <v-col lg="6" cols="12">
                        <v-card elevation="10" class="rounded-lg" link to="/admin/tags">
                            <v-card-text class="d-flex justify-space-between align-center">
                                <div>
                                    <strong>Tagovi</strong> <br>
                                    <span>Ukupan broj aktivnih tagova</span>
                                </div>
                                <v-avatar size="60" class='bg-warning'  style="border: 3px solid #444">
                                    <span class="fw-bold" style="color: navy"><i class="fa-solid fa-hashtag"></i> {{tags}}</span>
                                </v-avatar>
                            </v-card-text>
                            <v-card-actions class="d-flex justify-space-between">


                            </v-card-actions>
                        </v-card>
                    </v-col>
                    <v-col lg="6" cols="12">
                        <v-card elevation="10" class="rounded-lg" to="/admin/posts">
                            <v-card-text class="d-flex justify-space-between align-center">
                                <div>
                                    <strong>Postovi</strong> <br>
                                    <span>Ukupan broj aktivnih postova</span>
                                </div>
                                <v-avatar size="60" style="border: 3px solid #444; background-color: navy;">
                                    <span class="fw-bold" style="color: white"><i class="fa-solid fa-paste"></i> {{posts}}</span>
                                </v-avatar>
                            </v-card-text>
                            <v-card-actions class="d-flex justify-space-between">


                            </v-card-actions>
                        </v-card>
                    </v-col>
                    <v-col lg="6" cols="12">
                        <v-card elevation="10" class="rounded-lg" to="/admin/comments">
                            <v-card-text class="d-flex justify-space-between align-center">
                                <div>
                                    <strong>Komentari</strong> <br>
                                    <span>Ukupan broj aktivnih komentara</span>
                                </div>
                                <v-avatar size="60" style="border: 3px solid #444; background-color: navy;">
                                    <span class="fw-bold" style="color: white"><i class="fa-solid fa-comments"></i> {{comm}}</span>
                                </v-avatar>
                            </v-card-text>
                            <v-card-actions class="d-flex justify-space-between">


                            </v-card-actions>
                        </v-card>
                    </v-col>
                    <v-col lg="6" cols="12">
                        <v-card elevation="10" class="rounded-lg">
                            <v-card-text class="d-flex justify-space-between align-center">
                                <div>
                                    <strong>Slike</strong> <br>
                                    <span>Ukupan broj aktivnih slika</span>
                                </div>
                                <v-avatar size="60" class='bg-warning'  style="border: 3px solid #444">
                                    <span class="fw-bold" style="color: navy"><i class="fa-solid fa-images"></i> {{imgs}}</span>
                                </v-avatar>
                            </v-card-text>
                            <v-card-actions class="d-flex justify-space-between">


                            </v-card-actions>
                        </v-card>
                    </v-col>
                    
                </v-row>
            </v-col>
            <v-col cols="12" lg="5">
                <v-card elevation="10">
                    <v-card-title>Aktivnosti</v-card-title>
                    <v-card-text class="py-0">
                        <v-timeline class="tajmlajn" align-top dense>
                            
                            <v-timeline-item v-for='(l,i) in getShortList(1350)' :key='i' color="indigo" small v-if="l.useCaseName!='Search use case logs'">
                                <strong>{{l.useCaseName}}</strong>
                                <div class="text-caption">
                                  {{l.user}} - {{l.executionDateTime | formatDate2}}
                                </div>
                            </v-timeline-item>
                        
                        </v-timeline>
                    </v-card-text>
                </v-card>
            </v-col>
            <v-col>

                <h2 class="text-center py-5">Slike</h2>
                <v-card>
                    
                        <div class="elevation-5">
                            <v-data-table
                            :headers="headers"
                            :items="images.data"
                            hide-default-footer
                            class="elevation-1"
                            >
                             <template v-slot:item.path="{ item }">
                                <div class="py-2">

                                    <v-img :src="'http://localhost:5000/Images/'+item.path" width="100px"></v-img>
                                </div>
                            </template>

                            <template v-slot:item.createdAt="{ item }">
                                   <span>{{ item.createdAt | formatDate2 }}</span>
                            </template>

                            <template v-slot:item.user="{ item }">
                                   <span v-if="item.user">{{item.user}}</span>
                                   <span v-else><i class="fa-solid fa-xmark"></i></span>
                            </template>

                            <template v-slot:item.posts="{ item }">
                                    <template v-if="item.posts.length>0">
                                        <span>{{ item.posts.map(entry => entry.id).join(',') }}</span>
                                    </template>
                                       <span v-else><i class="fa-solid fa-xmark"></i></span>

                            </template>
                            <template v-slot:item.delete="{ item }">
                                   <v-btn @click='deletePhoto(item.id)' :disabled="item.user!='' || item.posts.length>0" class="bg-danger text-white ikona"><i class="fa-solid fa-trash-can"></i></v-btn>

                            </template>
                            
                            </v-data-table>
                            <div class="text-center pt-2">
                           <v-pagination class="pt-3"
                                v-model="currentPage"
                                :length="images.pagesCount"
                                @input="fetchImages()"
                            ></v-pagination> 
                            <p class="py-3">Ukupno slika: {{images.totalCount}}</p>
                            </div>
                        </div>
                </v-card>
                <template>
                        <h3 class="text-center py-4">Dodaj sliku</h3>
            <v-row justify="center">
                <v-dialog
                v-model="dialog"
                persistent
                max-width="600px"
                >
                <template v-slot:activator="{ on, attrs }">
                    <v-btn
                    color="yellow darken5"
                    dark
                    class="text-dark"
                    v-bind="attrs"
                    v-on="on"
                    >
                    Dodaj sliku
                    </v-btn>
                </template>
                <v-form ref="form" v-model='isFormValid' @submit.prevent="submitImage">
                <v-card>
                    <v-card-title>
                    <span class="text-h5">Dodavanje slike</span>
                    </v-card-title>
                    <v-card-text>
                    <v-container>
                        <v-row>
                        
                        <v-col cols="12">
                            <v-file-input
                              :rules="imageRules"
                              accept=".jpg,.jpeg,.png,.gif"
                              show-size
                              counter
                              v-model="imageToAdd"
                              @change="selectedImage"
                              label="Slika"
                              prepend-icon="mdi-camera">
                            </v-file-input>
                        </v-col>
                       
                       
                        </v-row>
                    </v-container>
                    </v-card-text>
                    <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                        color="blue darken-1"
                        text
                        @click="dialog = false"
                    >
                        Zatvori
                    </v-btn>
                    <v-btn
                        color="blue darken-1"
                        type="submit"
                        :disabled="!isFormValid"
                    >
                        Dodaj
                    </v-btn>
                    </v-card-actions>
                 
                </v-card>
                </v-form>
                </v-dialog>
            </v-row>
            </template>
            </v-col>
        </v-row>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';
     import axios from "axios"
     import moment from 'moment';
    export default {
        name: "Dashboard",
        
        data() {
            return {
                activityLogs:[],
                users:'',
                tags:'',
                cat:'',
                posts:'',
                comm:'',
                imgs:'',
                images:'',
                imageToAdd:null,
                isFormValid:true,
                dialog:false,
                mounted:false,
                currentPage:1,
                imageRules: [
                  imageToAdd =>   !imageToAdd ||  imageToAdd.size < 2e6 || 'Veličina slike mora da bude manja od 2 MB!',
                  imageToAdd =>   !!imageToAdd,
        
                  imageToAdd =>  !imageToAdd || (/\.(gif|GIF|jpe?g|JPE?G|png|PNG)$/i).test(imageToAdd.name) || 'Dozvoljene ekstenzije su .jpg, .jpeg, .png, .gif'
                ],
                headers: [
                    {
                        text: 'Id',
                        align: 'start',
                        sortable: false,
                        value: 'id',
                    },

                    {
                        text: 'Slika',
                        align: 'start',
                        sortable: false,
                        value: 'path',
                    },

                    {
                        text: 'Postovi',
                        align: 'start',
                        sortable: false,
                        value: 'posts',
                    },
                    {
                        text: 'Korisnik',
                        align: 'start',
                        sortable: false,
                        value: 'user',
                    },
                    {
                        text: 'Kreirana',
                        align: 'start',
                        sortable: false,
                        value: 'createdAt',
                    },
                    {
                        text: 'Obriši',
                        align: 'start',
                        sortable: false,
                        value: 'delete',
                    },
                    
                ],
            
            }
        },
        methods: {
            selectedImage(e){
          this.imageToAdd=e
     //     console.log(e)
        },
            getShortList(shortListSize) {
                 return this.activityLogs.slice(0, shortListSize);
            },
            async fetchImages(){
                 try{
                   const response= await axios.get('images?page='+this.currentPage)
                   //console.log(response.data)
                   this.images=response.data;
                   this.currentPage=response.data.currentPage;
                 }
                 catch(error){
                      console.log(error)
                 }
            },

              deletePhoto(id){
                  //console.log(id)

               axios.delete('images/'+id)
                    .then(()=>{
                     
                       this.fetchImages()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             } ,
             submitImage() {
                let formData = new FormData();
                formData.append('Image', this.imageToAdd,this.imageToAdd.name);
                 axios.post('images',formData)
                    .then(()=>{
                        this.fetchImages()
                        this.dialog = false
                        this.imageToAdd=null
                    })
                    .catch(err=>{
                      console.log(err)
                      
                      //console.log(this.feedback)
                    })
               
              },
        },

          mounted: function() {
           
                this.mounted=true;
                var tomorrow = new Date((new Date()).valueOf() + 1000*60*60*24);
                var yesterday= new Date((new Date()).valueOf() - 1000*60*60*24);

                //console.log(moment(String(yesterday)).format('MM-DD-YYYY'))
                //console.log('juce je'+moment(String(tomorrow)).format('MM-DD-YYYY'))
                axios.get('UseCaseLogs?DateFrom='+moment(String(yesterday)).format('MM-DD-YYYY')+'&DateTo='+moment(String(tomorrow)).format('MM-DD-YYYY'))
                    .then(r=>{
                           this.activityLogs=r.data;
                    })
                    .catch(err=>{
                      console.log(err)
                    // console.log(this.feedback)
                    })

                    axios.get('posts?isActive=true')
                    .then(r=>{
                           this.posts=r.data.totalCount;
                    })
                    .catch(err=>{
                      console.log(err)
                    // console.log(this.feedback)
                    })

                       axios.get('users?isActive=true')
                    .then(r=>{
                           this.users=r.data.totalCount;
                    })
                    .catch(err=>{
                      console.log(err)
                    // console.log(this.feedback)
                    })

                       axios.get('tags?isActive=true')
                    .then(r=>{
                           this.tags=r.data.totalCount;
                    })
                    .catch(err=>{
                      console.log(err)
                    // console.log(this.feedback)
                    })

                       axios.get('categories?isActive=true')
                    .then(r=>{
                           this.cat=r.data.totalCount;
                    })
                    .catch(err=>{
                      console.log(err)
                    // console.log(this.feedback)
                    })

                       axios.get('comments?isActive=true')
                    .then(r=>{
                           this.comm=r.data.totalCount;
                    })
                    .catch(err=>{
                      console.log(err)
                    // console.log(this.feedback)
                    })

                    axios.get('images?isActive=true')
                    .then(r=>{
                           console.log(r)
                           this.imgs=r.data.totalCount;
                           this.images=r.data; 
                         //  console.log(this.images)
                    })
                    .catch(err=>{
                      console.log(err)
                    // console.log(this.feedback)
                    })
         },
      
            computed:{
      ...mapGetters({
         authenticated:'auth/authenticated',
         user:'auth/user'
      })
    }

    }
</script>

<style scoped>
    .overlap-icon {
        position: absolute;
        top: -33px;
        text-align: center;
        padding-top: 12px;
    }
    .ikona{
        font-size: 18px;
    }

    .tajmlajn{
        max-height: 330px;
        overflow-y: auto;
    }
</style>