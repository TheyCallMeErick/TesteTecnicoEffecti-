import type { Item } from './item'

export type Licitacao = {
  id: string
  uasg: string
  pregao: string
  objeto: string
  orgao: string
  instituicao: string
  dataDisponibilizacaoEdital: string
  horaInicioEdital: string
  horaFimEdital: string
  enderecoEntrega: string
  telefone: string
  fax: string
  dataEntregaProposta: string
  horaEntregaProposta: string
  itens: Item[]
}
