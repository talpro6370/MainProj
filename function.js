function func(){
    fetch("http://localhost:58981/api/AnonymousFacade/GetAllAirlines").then(response=>{
        console.log(response);
    });
}
func();
