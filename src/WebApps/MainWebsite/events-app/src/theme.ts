import { createTheme } from '@mui/material/styles'
import { red } from '@mui/material/colors'
//https://github.com/codedthemes/berry-free-react-admin-template/blob/main/vite/src/layout/Customization/index.jsx
//https://mui.com/material-ui/react-container/

// A custom theme for this app
const theme = createTheme({
  palette: {
    primary: {
      main: '#556cd6',
    },
    secondary: {
      main: '#19857b',
    },
    error: {
      main: red.A400,
    },
  },
})

export default theme
