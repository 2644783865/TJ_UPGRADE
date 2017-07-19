///////////////////////////////////////////////////////////////////////////////////////// 
// SUPER TABLES - VERSION 0.25 - MIT Style License
// Copyright (c) 2008 MATT MURPHY --- visit www.matts411.com
///////////////////////////////////////////////////////////////////////////////////////// 
// TO CALL: 
// new superTable([string] tableId, { options });
//
// OPTIONS: (order does not matter )
// cssSkin : string
// headerRows : integer
// fixedCols : integer
// colWidths : integer array ( -1 for auto sizing )
// onFinish : function ( all this.variableNameHere variables created in this script can be used in this function, eg. this.columnWidths )
//
// EXAMPLES:
// myST = new superTable("myTableId");
//
// myST = new superTable("myTableId", {
//      cssSkin : "Sky",
//      fixedCols : 2,
//      colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
//      onFinish : function () {
//          alert(this.tBodyRowCount + " rows, " + this.columnCount + " columns converted.");
//      }
//  });
///////////////////////////////////////////////////////////////////////////////////////// 
// ISSUES / NOTES:
// 1. Does not check to see if rows are horizontally aligned if fixedCols > 0
// 2. colSpan not supported
// 3. No quirksmode support
///////////////////////////////////////////////////////////////////////////////////////// 

var superTables = [];
var superTable = function (tableId) {
    // add this instance to the supertables array
    var x = superTables.length; 
    this.index = x;
    superTables[x] = this;

    // initialize the parameters
    this.sourceTable = document.getElementById(tableId);
    this.hasOptions = (arguments.length > 1) ? true : false;
    this.skin = (this.hasOptions && arguments[1].cssSkin) ? "_" + arguments[1].cssSkin : "_Default";
    this.headerRows = (this.hasOptions && arguments[1].headerRows) ? parseInt(arguments[1].headerRows) : 1;
    this.fixedCols = (this.hasOptions && arguments[1].fixedCols) ? parseInt(arguments[1].fixedCols) : 0;
    this.columnWidths = (this.hasOptions && arguments[1].colWidths) ? arguments[1].colWidths : [];
    //添加行的高度
    this.rowHeights=(this.hasOptions && arguments[1].rowHeights)?arguments[1].rowHeights : [];
    //
    this.callbackFunc = (this.hasOptions && arguments[1].onFinish) ? arguments[1].onFinish : function () { return this; };
    var i, j, k;

    // get the column widths
    this.columnCount = (this.sourceTable.tHead) ? this.sourceTable.tBodies[0].rows[0].cells.length : this.sourceTable.tBodies[0].rows[this.headerRows].cells.length;
    this.columnWidthSum = 0;
    this.sourceTable.className = "sTempTable";
    for (i=0; i<this.columnCount; i++) {
        if (!this.columnWidths[i] || this.columnWidths[i] === -1) {
            this.columnWidths[i] = (this.sourceTable.tHead) ? this.sourceTable.tBodies[0].rows[0].cells[i].offsetWidth : this.sourceTable.tBodies[0].rows[this.headerRows].cells[i].offsetWidth;
        }
        this.columnWidthSum += this.columnWidths[i];
    }
    //获得行的高度
    this.rowCount=this.sourceTable.tBodies[0].rows.length;  //获得行数
    this.rowheightSum=0;
    for (i=0;i<this.rowCount;i++)
    {
       if(!this.rowHeights[i]||this.rowHeights[i]===-1)
       {
          this.rowHeights[i] = this.sourceTable.tBodies[0].rows[i].offsetHeight + 2;
       }
       this.rowheightSum += this.rowHeights[i];
    }
    // create the framework dom
    this.sParent = this.sourceTable.parentNode;
    this.sParentHeight = this.sParent.clientHeight - (this.sParent.offsetHeight - this.sParent.clientHeight);
    this.sParentWidth = this.sParent.clientWidth - (this.sParent.offsetWidth - this.sParent.clientWidth);
    this.sParent.removeChild(this.sourceTable);
	
	this.sBase = document.createElement("DIV");
	this.sFHeader = this.sBase.cloneNode(false);
	this.sHeader = this.sBase.cloneNode(false);
	this.sFData = this.sBase.cloneNode(false);
	this.sData = this.sBase.cloneNode(false);

    // create the row dom molds
    if (this.fixedCols > 0) { 
        var fRowMold = ["<TR>\n"]; 
        for (i=0; i<this.fixedCols; i++) {
            fRowMold.push("<TD><DIV>&nbsp;</DIV></TD>");
        }
        fRowMold.push("\n</TR>\n");
        fRowMold = fRowMold.join("");
    }
    var rowMold = ["<TR>\n"];
    for (i=0, j=(this.columnCount - this.fixedCols); i<j; i++) {
        rowMold.push("<TD><DIV>&nbsp;</DIV></TD>");
    }
    rowMold.push("\n</TR>\n");
    rowMold = rowMold.join("");

    // create the header dom framework
    if (this.fixedCols > 0) {
        var fHeaderMold = ["\n<TABLE><TBODY>\n" + fRowMold];
        for (i=1; i<this.headerRows; i++) {
            fHeaderMold.push(fRowMold);
        }
        fHeaderMold.push("</TBODY></TABLE>\n");
        this.sFHeader.innerHTML = fHeaderMold.join("");
        this.sFHeaderTable = this.sFHeader.getElementsByTagName("TABLE")[0];
    }
    var headerMold = ["\n<TABLE><TBODY>\n" + rowMold];
    for (i=1; i<this.headerRows; i++) {
        headerMold.push(rowMold);
    }
    headerMold.push("</TBODY></TABLE>\n");
    this.sHeader.innerHTML = headerMold.join("");
    this.sHeaderTable = this.sHeader.getElementsByTagName("TABLE")[0];

    // fill the header dom framework
    var rootDest, rootSource, dest, source;
    if (this.fixedCols > 0) {
        rootDest = this.sFHeaderTable.tBodies[0];
        rootSource = (this.sourceTable.tHead) ? this.sourceTable.tHead : this.sourceTable.tBodies[0];
        for (i=0; i<this.headerRows; i++) {
            dest = rootDest.rows[i];
            dest.style.height = this.rowHeights[i] + "px";  //设定表头的高度
            source = rootSource.rows[i];
            for (j=0; j<this.fixedCols; j++) {
                try {
                    dest.cells[j].firstChild.innerHTML = source.cells[j].innerHTML;
                    dest.cells[j].style.width = this.columnWidths[j] + "px";
                } catch (e) { }
            }
        }
    }
    rootDest = this.sHeaderTable.tBodies[0];
    rootSource = (this.sourceTable.tHead) ? this.sourceTable.tHead : this.sourceTable.tBodies[0];
    for (i=0; i<this.headerRows; i++) {
        dest = rootDest.rows[i];
        dest.style.height = this.rowHeights[i] + "px";  //设定表头的高度
        source = rootSource.rows[i];
        for (j=0, k=(this.columnCount - this.fixedCols); j<k; j++) {
            try {
                dest.cells[j].firstChild.innerHTML = source.cells[j + this.fixedCols].innerHTML;
                dest.cells[j].style.width = this.columnWidths[j + this.fixedCols] + "px";
            } catch (e) { }
        }
    }

    // create the data dom framework
    this.tBodyRowCount = this.sourceTable.tBodies[0].rows.length;
    if (this.fixedCols > 0) {
        var fDataMold = ["\n<TABLE><TBODY>\n"];
        for (i=(this.sourceTable.tHead) ? 0 : this.headerRows; i<this.tBodyRowCount; i++) {
            fDataMold.push(fRowMold);
        }
        fDataMold.push("</TBODY></TABLE>\n");
        this.sFData.innerHTML = fDataMold.join("");
        this.sFDataTable = this.sFData.getElementsByTagName("TABLE")[0];
    }
    var dataMold = ["\n<TABLE><TBODY>\n"];
    for (i=(this.sourceTable.tHead) ? 0 : this.headerRows; i<this.tBodyRowCount; i++) {
        dataMold.push(rowMold);
    }
    dataMold.push("</TBODY></TABLE>\n");
    this.sData.innerHTML = dataMold.join("");
    this.sDataTable = this.sData.getElementsByTagName("TABLE")[0];

    // fill the data dom framework
    if (this.fixedCols > 0) {
        rootDest = this.sFDataTable.tBodies[0];
        rootSource = this.sourceTable.tBodies[0];
        for (i=(this.sourceTable.tHead) ? 0 : this.headerRows; i<this.tBodyRowCount; i++) {
            dest = rootDest.rows[(this.sourceTable.tHead) ? i : i - this.headerRows];
            dest.style.height = this.rowHeights[i] + "px";  //设定行的高度
            source = rootSource.rows[i];
            for (j=0; j<this.fixedCols; j++) {
                try {
                    dest.cells[j].firstChild.innerHTML = source.cells[j].innerHTML;
                } catch (e) { }
            }
        }
        for (i=0; i<this.fixedCols; i++) {
            rootDest.rows[0].cells[i].style.width = this.columnWidths[i] + "px";
        }
    }
    rootDest = this.sDataTable.tBodies[0];
    rootSource = this.sourceTable.tBodies[0];
    for (i=(this.sourceTable.tHead) ? 0 : this.headerRows; i<this.tBodyRowCount; i++) {
        dest = rootDest.rows[(this.sourceTable.tHead) ? i : i - this.headerRows];
        dest.style.height = this.rowHeights[i] + "px";  //设定行的高度
        source = rootSource.rows[i];
        for (j=0, k=(this.columnCount - this.fixedCols); j<k; j++) {
            try {
                dest.cells[j].firstChild.innerHTML = source.cells[j + this.fixedCols].innerHTML;
            } catch (e) { }
        }
    }
    for (i=0, j=(this.columnCount - this.fixedCols); i<j; i++) {
        rootDest.rows[0].cells[i].style.width = this.columnWidths[i + this.fixedCols] + "px";
    }

    // place everything into the page
	if (this.fixedCols > 0) { this.sBase.appendChild(this.sFHeader); }
	this.sBase.appendChild(this.sHeader);
	if (this.fixedCols > 0) { this.sBase.appendChild(this.sFData); }
	this.sBase.appendChild(this.sData);

    // style the tables
	this.sBase.className = "sBase" + this.skin;
	this.sFHeader.className = "sFHeader" + this.skin;
	this.sHeader.className = "sHeader" + this.skin;
	this.sFData.className = "sFData" + this.skin;
	this.sData.className = "sData" + this.skin;
    
    // render and get needed dimensions
    this.sParent.appendChild(this.sBase);
    if (this.fixedCols > 0) {
        this.sFHeaderTableOffsetWidth = this.sFHeaderTable.offsetWidth;
    }
    this.sHeaderTableOffsetHeight = this.sHeaderTable.offsetHeight;
    
    // align the tables
	if (this.fixedCols > 0) {
        this.sHeader.style.right = (this.sFHeaderTableOffsetWidth * -1) + "px";
        this.sData.style.marginLeft = this.sFHeaderTableOffsetWidth + "px"; 
        this.sData.style.width = (this.sParentWidth - this.sFHeaderTableOffsetWidth) + "px";
    }
    this.sData.style.height = (this.sParentHeight - this.sHeaderTableOffsetHeight) + "px";
    this.sData.style.overflow = "auto"; 
    if (this.fixedCols > 0) {
        if (this.sParentWidth < this.columnWidthSum) {
            this.sHorOverflow = document.createElement("DIV");
            this.sHorOverflow.className = "sHorOverflow";
            this.sHorOverflow.style.width = this.sFHeaderTableOffsetWidth + "px";
            this.sBase.appendChild(this.sHorOverflow);
        }
    }
   if (this.sParentHeight < this.rowheightSum ) {
      this.sVertOverflow = document.createElement("DIV");
      this.sVertOverflow.className = "sVertOverflow";
      this.sVertOverflow.style.height = this.sHeaderTableOffsetHeight + "px";
      this.sBase.appendChild(this.sVertOverflow);
    }
//    if (this.sParentHeight < (this.tBodyRowCount * this.sHeaderTableOffsetHeight)) {
//        this.sVertOverflow = document.createElement("DIV");
//        this.sVertOverflow.className = "sVertOverflow";
//        this.sVertOverflow.style.height = this.sHeaderTableOffsetHeight + "px";
//        this.sBase.appendChild(this.sVertOverflow);
//    }
    
    // set up table events
    if (this.fixedCols > 0) {
        this.sData.onscroll = function () {
            superTables[x].sHeader.style.right = (superTables[x].sData.scrollLeft - superTables[x].sFHeaderTableOffsetWidth) + "px";
            superTables[x].sFData.style.top = ((superTables[x].sData.scrollTop * -1) + superTables[x].sHeaderTableOffsetHeight) + "px";
            return true;
        }
    } else {
        this.sData.onscroll = function () {
        	superTables[x].sHeader.style.right = superTables[x].sData.scrollLeft + "px";
    		return true;
        }
    }
    
    return this.callbackFunc();
}


