function ekleUretimKaydi() {
    let kayitNo = document.getElementById("kayitNo").value;
    let baslangic = document.getElementById("baslangic").value;
    let bitis = document.getElementById("bitis").value;
    let statu = document.getElementById("statu").value;
    let durusNedeni = document.getElementById("durusNedeni").value || "-";

    if (!kayitNo || !baslangic || !bitis) {
        alert("Lütfen tüm alanları doldurun.");
        return;
    }

    let table = document.getElementById("uretimTablosu").getElementsByTagName('tbody')[0];
    let newRow = table.insertRow();
    newRow.innerHTML = `<td>${kayitNo}</td><td>${baslangic}</td><td>${bitis}</td><td>${statu}</td><td>${durusNedeni}</td>`;
}

function hesaplaTablo() {
    let uretimTable = document.getElementById("uretimTablosu").getElementsByTagName('tbody')[0];
    let islenmisTable = document.getElementById("islenmisTablo").getElementsByTagName('tbody')[0];

    islenmisTable.innerHTML = "";

    for (let row of uretimTable.rows) {
        let baslangic = row.cells[1].innerText;
        let bitis = row.cells[2].innerText;
        let statu = row.cells[3].innerText;
        let durusNedeni = row.cells[4].innerText;

        let newRow = islenmisTable.insertRow();
        newRow.innerHTML = `<td>${baslangic}</td><td>${bitis}</td><td>${statu}</td><td>${durusNedeni}</td>`;
    }
}
