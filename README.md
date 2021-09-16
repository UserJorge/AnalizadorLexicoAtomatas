# Lenguajes y Autómatas 2
## Nombre del Proyecto: AnalizadorLexicoAutomatas (CalcFun)
### Proyecto Analizador Léxico

- Jorge Moreno 
- Ulises Perez
- Humerto Ramos
- Alejandro Elguezabal

Para colocar una sentancia y calcular el resultado.

primero colocas el texto como el siguiente 
```
SUM(8,2);
```
para mostrar el resultado de la operación primero se escanea el boton de léxico y después compilar.
*hasta ahora solo tenemos la suma simple como operacion que genera un resultado.

## Control de errores:

- si no hay parentesis de apertura o cerradura al final se generará un error.
- si no hay punto y coma al finalizar (;)
- Los datos son del 0 al 9 no admitiendo numeros negativos,
- (en el futuro soportará la resta)

Si la sentancia no está correctamente escrita en algunos casos no se mostrará el error y 
no cargará los tokens ya que no cumple con una sentencia correcta.
