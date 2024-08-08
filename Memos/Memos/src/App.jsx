import { Button, Input, Textarea } from "@chakra-ui/react"
import "./App.css"

function App() {
  return <section>
    <div>
      <form>
        <h3>
          Создание заметки
          <Input placeholder="Название"/>
          <Textarea placeholder="Описание"/>
          <Button>Создать</Button>
        </h3>
      </form>
    </div>
  </section>
}

export default App
