public class BotComanderB : BotCommander
{
    protected override void ListaInstrucciones()
    {
        /*
         *  Girar(string direccion);
         *  Avanzar(int pasos);
         */
        //ejemplo
        
        Avanzar(2);
        Girar("der");
        Avanzar(5);
    }
}