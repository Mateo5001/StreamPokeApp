# Paletas de color de PokéStream

Definidas en `PokeStreamTheme.cs` (MudBlazor `PaletteLight` / `PaletteDark`). Objetivo: reflejar juventud y diversión, con el universo Pokémon como referencia.

## Paleta de día — "Naranja Charmander"

Color principal: naranja. Fondo crema cálido con acentos azul Pokédex y amarillo Pikachu.

| Atributo | Color | Uso |
|---|---|---|
| Primary | `#FF7A1A` | Botones principales, elementos activos |
| PrimaryContrastText | `#2B1B0E` | Texto sobre Primary (el blanco solo da ~2,6:1) |
| Secondary | `#2E6BFF` | Acciones secundarias, enlaces (azul Pokédex) |
| SecondaryContrastText | `#FFFFFF` | Texto sobre Secondary |
| Tertiary | `#FFD60A` | Acentos, insignias como Shiny (amarillo Pikachu) |
| TertiaryContrastText | `#2B1B0E` | Texto sobre Tertiary |
| TextPrimary | `#2B1B0E` | Texto principal |
| Background | `#FBE9D6` | Fondo de la aplicación |
| Surface | `#FFFFFF` | Tarjetas y paneles |
| AppbarBackground | `#FF7A1A` | Barra superior |
| AppbarText | `#2B1B0E` | Texto de la barra superior |
| DrawerBackground | `#F6D9BC` | Menú lateral |

## Paleta de noche — "Azul Noche Squirtle"

Color principal: azul oscuro. El naranja y el amarillo eléctrico resaltan sobre el fondo nocturno.

| Atributo | Color | Uso |
|---|---|---|
| Primary | `#3B55E6` | Botones principales, elementos activos |
| PrimaryContrastText | `#FFFFFF` | Texto sobre Primary |
| Secondary | `#FF8A2A` | Acciones secundarias, chispa de color |
| SecondaryContrastText | `#1A0F05` | Texto sobre Secondary |
| Tertiary | `#FFD60A` | Acentos, insignias como Shiny |
| TertiaryContrastText | `#1A0F05` | Texto sobre Tertiary |
| TextPrimary | `#EEF1FF` | Texto principal |
| Background | `#0A0F24` | Fondo de la aplicación |
| Surface | `#131A3A` | Tarjetas y paneles |
| AppbarBackground | `#101842` | Barra superior (cercana al drawer) |
| AppbarText | `#EEF1FF` | Texto de la barra superior |
| DrawerBackground | `#0F1631` | Menú lateral |

## Criterios

- Los colores claros y saturados (naranja, amarillo) llevan texto oscuro para mantener la legibilidad.
- El amarillo Pikachu `#FFD60A` es común a ambas paletas para dar identidad de marca.
- Día y noche se invierten: el naranja protagonista de día pasa a acento secundario de noche, y el azul acento de día pasa a protagonista.
