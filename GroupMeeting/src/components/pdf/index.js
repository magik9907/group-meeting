import { jsPDF } from 'jspdf'
import QRCode from 'qrcode'

class PDFTicket {
  width = 842
  height = 595
  aqua = [30, 143, 235]
  green = [80, 246, 42]
  darkblue = [25, 0, 168]

  constructor(data) {
    this.Data = data;
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

  QRCode() {
    let s
    QRCode.toDataURL('http://www.google.com', function (err, string) {
      if (err) throw err
      s = string
    })
    this.doc.addImage(s, this.width / 2 - 50, this.height - 150, 100, 100)
  }

  Create() {
    var centerHorizontal = this.width / 2
    this.Circle(this.width, this.height, this.green)
    this.Circle(this.width, 0, this.aqua)
    this.Text('Bilet', 30, centerHorizontal, 50)
    this.Text('dla Pan/Pani', 25, centerHorizontal, 90)
    this.Text(
      this.Data.Name,
      30,
      centerHorizontal,
      150,
      undefined,
      ['Montserrat-Bold', 'normal'],
      [255, 255, 255]
    )
    this.Text('na', 25, centerHorizontal, 200)
    this.Text(
      this.Data.MeetingName,
      30,
      centerHorizontal,
      250,
      { align: 'center', maxWidth: this.width * 0.9 },
      ['Montserrat-Bold', 'normal'],
      this.darkblue
    )

    this.Text(`w dniu: ${this.Data.Date}`, 25, centerHorizontal, 400)

    this.QRCode()

    this.doc.output('dataurlnewwindow', { filename: 'Ticket' })
  }
}

document
  .querySelector('#GeneratePdfButton')
  .addEventListener('click', (event) => {
    const id = event.target.dataset['id']
    fetch(`${location.protocol}//${location.host}/api/ticket/${id}`)
      .then((response) => response.json())
      .then((data) => {
        const pdf = new PDFTicket(data)
        pdf.Create()
      })
  })
