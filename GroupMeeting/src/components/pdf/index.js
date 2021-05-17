import { jsPDF } from 'jspdf'

class PDFTicket {
  width = 842
  height = 595
  aqua = [30, 143, 235]
  green = [80, 246, 42]
  darkblue = [25, 0, 168]
  constructor() {
    this.doc = new jsPDF({
      format: [this.width, this.height],
      title: 'Ticket',
      unit: 'pt',
      orientation: 'l',
    })
  }
  Circle(width, height, color) {
    let x = width / 2
    let y = height
    let r = x
    this.doc.setLineWidth(0)
    this.doc.setDrawColor(0)
    this.doc.setFillColor(...color)
    this.doc.circle(x, y, r, 'F')
  }

  Text(
    text,
    size,
    width,
    height,
    options = {
      align: 'center',
    },
    font = ['Montserrat-Medium', 'normal'],
    fontColor = [0, 0, 0]
  ) {
    this.doc.setTextColor(...fontColor)
    this.doc.setFont(...font)
    this.doc.setFontSize(size)
    this.doc.text(text, width, height, options)
  }

  Create() {
    var centerHorizontal = this.width / 2
    this.Circle(this.width, this.height, this.green)
    this.Circle(this.width, 0, this.aqua)
    this.Text('Bilet', 30, centerHorizontal, 50)
    this.Text('dla Pan/Pani', 25, centerHorizontal, 90)
    this.Text(
      'Agnieszka Radwańska',
      30,
      centerHorizontal,
      150,
      undefined,
      ['Montserrat-Bold', 'normal'],
      [255, 255, 255]
    )
    this.Text('na', 25, centerHorizontal, 200)
    this.Text(
      'Spotkanie JS developerów ddddddddddddddddddddddddz pasją nauki tej sztuki',
      30,
      centerHorizontal,
      250,
      { align: 'center', maxWidth: this.width * 0.9 },
      ['Montserrat-Bold', 'normal'],
      this.darkblue
    )

    this.Text(
      'w dniu: 25.04.2020 14:00',
      25,
      centerHorizontal,
      400,      
    )

    this.doc.output('dataurlnewwindow', { filename: 'Ticket' })
  }
}

document.querySelector('#GeneratePdfButton').addEventListener('click', () => {
  const pdf = new PDFTicket()
  pdf.Create()
})
