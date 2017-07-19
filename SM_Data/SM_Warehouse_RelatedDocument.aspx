<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true" CodeBehind="SM_Warehouse_RelatedDocument.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_RelatedDocument" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    
    <script type="text/javascript" language="javascript">
        function dispplan() {
            var i = document.getElementById("Plan");
            var j = document.getElementById("DispPlan");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏需用计划";
            }
            else {
                i.style.display = "none";
                j.value = "显示需用计划";
            }
        }

       function dispzy() {
            var i = document.getElementById("ZY");
            var j = document.getElementById("DispZY");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏占用";
            }
            else {
                i.style.display = "none";
                j.value = "显示占用";
            }
        }
        
         function dispxsdy() {
            var i = document.getElementById("XSDY");
            var j = document.getElementById("DispXSDY");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏相似代用";
            }
            else {
                i.style.display = "none";
                j.value = "显示相似代用";
            }
        }

        function dispcmp() {
            var i = document.getElementById("CMP");
            var j = document.getElementById("DispCMP");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏询比价";
            }
            else {
                i.style.display = "none";
                j.value = "显示询比价";
            }
        }

        function disporder() {
            var i = document.getElementById("Order");
            var j = document.getElementById("DispOrder");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏订单";
            }
            else {
                i.style.display = "none";
                j.value = "显示订单";
            } 
        }

        function dispinput() {
            var i = document.getElementById("Input");
            var j = document.getElementById("DispInput");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏入库单";
            }
            else {
                i.style.display = "none";
                j.value = "显示入库单";
            } 
        }

        function dispoutput() {
            var i = document.getElementById("Output");
            var j = document.getElementById("DispOutput");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏出库单";
            }
            else {
                i.style.display = "none";
                j.value = "显示出库单";
            } 
        }

        function dispmto() {
            var i = document.getElementById("MTO");
            var j = document.getElementById("DispMTO");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏MTO调整单";
            }
            else {
                i.style.display = "none";
                j.value = "显示MTO调整单";
            } 
        }

        function dispal() {
            var i = document.getElementById("AL");
            var j = document.getElementById("DispAL");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏调拨单";
            }
            else {
                i.style.display = "none";
                j.value = "显示调拨单";
            } 
        }

        function dispinvoice() {
            var i = document.getElementById("Invoice");
            var j = document.getElementById("DispInvoice");
            if (i.style.display == "none") {
                i.style.display = 'block';
                j.value = "隐藏发票";
            }
            else {
                i.style.display = "none";
                j.value = "显示发票";
            } 
        }
    </script>

    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
    <asp:Panel ID="Operation" runat="server">
    <table width="100%">
    <tr>
        <td>
            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="true"></asp:Label>
        </td>
        <td align="right"></td>
    </tr>
    </table>
    </asp:Panel>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper" >
    <div class="box-outer">
    
    <table width="100%">
        <tr>
            <td style=" font-size:x-large; text-align:center;" colspan="2">
                关联单据查询结果
            </td>
        </tr>
        <tr style="width:100%">
            <td align="left" style="width:80%;">
               <%-- <input type="button" id="DispPlan" value="隐藏需用计划" onclick="dispplan()" visible="false" />--%>
                <input type="button" id="DispZY" value="隐藏占用" onclick="dispzy()" />
                
                |<input type="button" id="DispXSDY" value="隐藏相似代用" onclick="dispxsdy()" />
                |<input type="button" id="DispCMP" value="隐藏询比价" onclick="dispcmp()" />
                |<input type="button" id="DispOrder" value="隐藏订单" onclick="disporder()" />
                |<input type="button" id="DispInput" value="隐藏入库单" onclick="dispinput()" />                
               |<input type="button" id="DispOutput" value="隐藏出库单" onclick="dispoutput()" />
                |<input type="button" id="DispMTO" value="隐藏MTO调整单" onclick="dispmto()" />
               |<%--<input type="button" id="DispAL" value="隐藏调拨单" onclick="dispal()"  />
                |--%><input type="button" id="DispInvoice" value="隐藏发票" onclick="dispinvoice()" />
            </td>
            <td>
                计划跟踪号：<asp:Label ID="LabelPTC" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    
        <br />
        <hr />
        <br />
    
      <div id="Div_HZ" class="container">
       <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <caption>汇总</caption>
           <asp:Repeater ID="RepeaterHZ" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>                                 
                <td><strong>计划跟踪号</strong></td>
                <td><strong>物料编码</strong></td>
                <td><strong>物料名称</strong></td>
                <td><strong>规格型号</strong></td>
                <td><strong>单位</strong></td>
                <td><strong>计划需用数量</strong></td>
                <td><strong>占用数量</strong></td>
                <td><strong>相似代用数量</strong></td>
                <td><strong>订货数量</strong></td>
                <td><strong>入库数量</strong></td>
                <td><strong>出库数量</strong></td>
                <td><strong>MTO数量</strong></td>
            </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                    <td><%#Container.ItemIndex + 1 %></td>
                    <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelPlanNum" runat="server" Text='<%#Eval("PlanNum")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelZYNum" runat="server" Text='<%#Eval("ZYNum")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelDYNum" runat="server" Text='<%#Eval("DYNum")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelDHNum" runat="server" Text='<%#Eval("DHNum")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelRKNum" runat="server" Text='<%#Eval("RKNum")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelCKNum" runat="server" Text='<%#Eval("CKNum")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMTONum" runat="server" Text='<%#Eval("MTONum")%>'></asp:Label></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
          </table>
        </div>
    <%--<hr />--%>
    
    <div id="Plan" class="container" style="display: none">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
    <caption>物料需用计划</caption>
        <asp:Repeater ID="RepeaterPlan" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td><strong>项目</strong></td>
                <td><strong>工程</strong></td>                
                <td><strong>申请部门</strong></td>
                <td><strong>申请人</strong></td>
                <td><strong>申请日期</strong></td>                                    
                <td><strong>计划跟踪号</strong></td>
                <td><strong>物料编码</strong></td>
                <td><strong>物料名称</strong></td>
                <td><strong>规格型号</strong></td>
                <td><strong>单位</strong></td>
                <td><strong>计划需用数量</strong></td>
                <td><strong>计划需用重量</strong></td>
                <td><strong></strong></td>
            </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                    <td><%#Container.ItemIndex + 1 %></td>
                    <td><asp:Label ID="LabelProject" runat="server" Text='<%#Eval("Project")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelEng" runat="server" Text='<%#Eval("Eng")%>'></asp:Label></td>                    
                    <td><asp:Label ID="LabelDep" runat="server" Text='<%#Eval("Dep")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelApplicant" runat="server" Text='<%#Eval("Applicant")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelNum" runat="server" Text='<%#Eval("Num")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelWeight" runat="server" Text='<%#Eval("Weight")%>'></asp:Label></td>
                    <td style="color:#FF0000;"><asp:HyperLink ID="HyperLinkRead" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?mp_id="+Eval("PH")%>'  runat="server">查看</asp:HyperLink></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        </div>
        
<hr />

<div id="ZY" class="container">

<table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
    <caption>占用</caption>
        <asp:Repeater ID="RepeaterZY" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td><strong>批号</strong></td>
                <td><strong>项目</strong></td>
                <td><strong>工程</strong></td>                                                 
                <td><strong>计划跟踪号</strong></td>
                <td><strong>物料编码</strong></td>
                <td><strong>物料名称</strong></td>
                <td><strong>规格型号</strong></td>
                <td><strong>长</strong></td>
                <td><strong>宽</strong></td>
                <td><strong>采购单位</strong></td>
                <td><strong>计划需用数量</strong></td>
                <td><strong>占用数量</strong></td>
                <td><strong></strong></td>
            </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                    <td><%#Container.ItemIndex + 1 %></td>
                    <td><asp:Label ID="LabelPH" runat="server" Text='<%#Eval("planno")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelProject" runat="server" Text='<%#Eval("pjnm")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelEng" runat="server" Text='<%#Eval("engnm")%>'></asp:Label></td>                    
                    <td><asp:Label ID="LabelDep" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("marid")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("marnm")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("margg")%>'></asp:Label></td>
                    
                     <td><asp:Label ID="LabelLength" runat="server" Text='<%#Eval("length")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelWidth" runat="server" Text='<%#Eval("width")%>'></asp:Label></td>
                    
                    <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("marunit")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelNum" runat="server" Text='<%#Eval("num")%>'></asp:Label></td>
                    <td><asp:Label ID="LabelZYNum" runat="server" Text='<%#Eval("usenum")%>'></asp:Label></td>
                    <td style="color:#FF0000;"></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

</div>


        <hr />
        
        <div id="XSDY" class="container">

        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <caption>相似代用</caption>
                <asp:Repeater ID="RepeaterXSDY" runat="server">
                    <HeaderTemplate>
                    <tr align="center" class="tableTitle1">
                        <td><strong></strong></td>
                        <td><strong>代用单号</strong></td>
                        <td><strong>项目</strong></td>
                        <td><strong>工程</strong></td>                                                 
                        <td><strong>计划跟踪号</strong></td>
                        <td><strong>物料编码</strong></td>
                        <td><strong>物料名称</strong></td>
                        <td><strong>规格型号</strong></td>
                        <td><strong>长</strong></td>
                        <td><strong>宽</strong></td>
                        <td><strong>单位</strong></td>
                        <td><strong>计划需用数量</strong></td>
                        <td><strong>代用数量</strong></td>
                        <td><strong></strong></td>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td><%#Container.ItemIndex + 1 %></td>
                             <td><asp:Label ID="LabelDYDH" runat="server" Text='<%#Eval("mpcode")%>'></asp:Label></td>
                            <td><asp:Label ID="LabelProject" runat="server" Text='<%#Eval("pjnm")%>'></asp:Label></td>
                            <td><asp:Label ID="LabelEng" runat="server" Text='<%#Eval("engnm")%>'></asp:Label></td>                    
                           
                            <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label></td>
                            
                            <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("marid")%>'></asp:Label></td>
                            <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("marnm")%>'></asp:Label></td>
                            <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("marguige")%>'></asp:Label></td>
                            
                            <td><asp:Label ID="LabelLength" runat="server" Text='<%#Eval("length")%>'></asp:Label></td>
                            <td><asp:Label ID="LabelWidth" runat="server" Text='<%#Eval("width")%>'></asp:Label></td>
                            
                            <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("marcgunit")%>'></asp:Label></td>
                            <td><asp:Label ID="LabelNum" runat="server" Text='<%#Eval("num")%>'></asp:Label></td>
                            <td><asp:Label ID="LabelDYNum" runat="server" Text='<%#Eval("usenum")%>'></asp:Label></td>
                            <td style="color:#FF0000;"></td>
                        </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

        </div>

        <hr />
       
    
    <div id="CMP" class="container">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <caption>询比价单</caption>
        <asp:Repeater ID="RepeaterCMP" runat="server" >
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>                                
                <td><strong>项目名称</strong></td>
                <td><strong>询比价单编号</strong></td>
                <td><strong>制单人</strong></td>                
                <td><strong>制单时间</strong></td>                 
                <td><strong>询比价单状态</strong></td>
                <td><strong>计划跟踪号</strong></td>                
                <td><strong>物料代码</strong></td>
                <td><strong>物料名称</strong></td>             
                <td><strong>型号规格</strong></td>
                <td><strong>单位</strong></td>
                <td><strong>数量</strong></td>      
                <td><strong>报价状态</strong></td>
                <td><strong></strong></td>               
            </tr>
            </HeaderTemplate>
            <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">        
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelProject" runat="server" Text='<%#Eval("Project")%>'></asp:Label></td>
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDoc" runat="server" Text='<%#Eval("Doc")%>'></asp:Label></td>                   
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label></td>                
                <td><asp:Label ID="LabelCMPState" runat="server" Text='<%#convertCMP((string)Eval("CMPState")) %>'></asp:Label></td>
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                <td><asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>'></asp:Label></td>                
                <td><asp:Label ID="LabelState" runat="server" Text='<%#Eval("State") %>'></asp:Label></td>
                <td style="color:#FF0000;"><asp:HyperLink ID="HyperLinkRead" NavigateUrl='<%#"~/PC_Data/PC_TBPC_PurOrder.aspx?FLAG=ORDERLIST&orderno="+Eval("Code")+"&state=111"%>'  runat="server">查看</asp:HyperLink></td>                                                              
            </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    

        <hr />


    <div id="Order" class="container">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <caption>采购订单</caption>
        <asp:Repeater ID="RepeaterOrder" runat="server" >
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>                                
                <td><strong>供应商</strong></td>
                <td><strong>日期</strong></td>
                <td><strong>编号</strong></td>
                <td><strong>部门</strong></td>                
                <td><strong>业务员</strong></td>                 
                <td><strong>订单状态</strong></td>
                <td><strong>计划跟踪号</strong></td>                
                <td><strong>物料代码</strong></td>
                <td><strong>物料名称</strong></td>             
                <td><strong>型号规格</strong></td>
                <td><strong>单位</strong></td>
                <td><strong>订货数量</strong></td>
                <td><strong>到货数量</strong></td>
                <td><strong>交货日期</strong></td>          
                <td><strong>行业务状态</strong></td>
                <td><strong>单价</strong></td>
                <td><strong>税率</strong></td>
                <td><strong>含税单价</strong></td>
                <td><strong>金额</strong></td>
                <td><strong>含税金额</strong></td>
                <td><strong></strong></td>               
            </tr>
            </HeaderTemplate>
            <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">        
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelSupplier" runat="server" Text='<%#Eval("Supplier")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label></td>
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDep" runat="server" Text='<%#Eval("Dep")%>'></asp:Label></td>                   
                <td><asp:Label ID="LabelClerk" runat="server" Text='<%#Eval("Clerk")%>'></asp:Label></td>                
                <td><asp:Label ID="LabelOrderState" runat="server" Text='<%#convertOrder((string)Eval("OrderState")) %>'></asp:Label></td>
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                <td><asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>'></asp:Label></td>                
                <td><asp:Label ID="LabelArrivedNumber" runat="server" Text='<%#Eval("ArrivedNumber")%>'></asp:Label></td>
                <td><asp:Label ID="LabelArrivedDate" runat="server" Text='<%#Eval("ArrivedDate")%>'></asp:Label></td>
                <td><asp:Label ID="LabelItemState" runat="server" Text='<%#convertItem((string)Eval("ItemState")) %>'></asp:Label></td>
                <td><asp:Label ID="LabelUnitPrice" runat="server" Text='<%#Eval("UnitPrice")%>'></asp:Label></td>
                <td><asp:Label ID="LabelTaxRate" runat="server" Text='<%#Eval("TaxRate")%>'></asp:Label>%</td>
                <td><asp:Label ID="LabelCTUP" runat="server" Text='<%#Eval("CTUP")%>'></asp:Label></td>                
                <td><asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label></td>
                <td><asp:Label ID="LabelCTA" runat="server" Text='<%#Eval("CTA")%>'></asp:Label></td>
                <td style="color:#FF0000;"><asp:HyperLink ID="HyperLinkRead" NavigateUrl='<%#"~/PC_Data/PC_TBPC_PurOrder.aspx?FLAG=ORDERLIST&orderno="+Eval("Code")+"&state=111"%>'  runat="server">查看</asp:HyperLink></td>                                                              
            </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    

        <hr />

 
    <div id="Input" class="container">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <caption>入库单</caption>
        <asp:Repeater ID="RepeaterIn" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td><strong>日期</strong></td>
                <td><strong>审核状态</strong></td>
                <td><strong>单据编号</strong></td>
                <td><strong>供应商</strong></td>
                <td><strong>收料仓库</strong></td>
                <td><strong>计划跟踪号</strong></td>                
                <td><strong>物料代码</strong></td>
                <td><strong>物料名称</strong></td>
                <td><strong>型号规格</strong></td>
                <td><strong>单位</strong></td>
                <td><strong>实收数量</strong></td>                
                <td><strong>单价</strong></td>                  
                <td><strong>金额</strong></td>
                <td><strong>部门</strong></td>                  
                <td><strong>业务员</strong></td>     
                <td><strong></strong></td>                                
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label></td>
                <td><asp:Label ID="LabelState" runat="server"  Text='<%#convertIn((string)Eval("State"))%>'></asp:Label></td>
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
                <td><asp:Label ID="LabelSupplier" runat="server" Text='<%#Eval("Supplier")%>'></asp:Label></td>
                <td><asp:Label ID="LabelWarehouse" runat="server" Text='<%#Eval("Warehouse")%>'></asp:Label></td>
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                <td><asp:Label ID="LabelRN" runat="server" Text='<%#Eval("RN")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnitPrice" runat="server" Text='<%#Eval("UnitPrice")%>'></asp:Label></td>
                <td><asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDep" runat="server" Text='<%#Eval("Dep")%>'></asp:Label></td>
                <td><asp:Label ID="LabelClerk" runat="server" Text='<%#Eval("Clerk")%>'></asp:Label></td>          
                <td style="color:#FF0000;"><asp:HyperLink ID="HyperLinkRead" runat="server" NavigateUrl='<%#"~/SM_Data/SM_WarehouseIN_WG.aspx?FLAG=READ&&ID="+Eval("Code")%>'>查看</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    

        <hr />


    <div id="Output" class="container">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <caption>出库单</caption>
        <asp:Repeater ID="RepeaterOut" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td><strong>日期</strong></td>
                <td><strong>审核状态</strong></td>
                <td><strong>单据编号</strong></td>
                <td><strong>领料部门</strong></td>
                <td><strong>发料仓库</strong></td>
                <td><strong>计划跟踪号</strong></td>                
                <td><strong>物料代码</strong></td>
                <td><strong>物料名称</strong></td>
                <td><strong>型号规格</strong></td>
                <td><strong>单位</strong></td>
                <td><strong>即时库存数量</strong></td>
                <td><strong>实发数量</strong></td>                
                <td><strong>单价</strong></td>                  
                <td><strong>金额</strong></td>
                <td><strong></strong></td>                                
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label></td>
                <td><asp:Label ID="LabelState" runat="server" Text='<%#convertOut((string)Eval("State"))%>'></asp:Label></td>                
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDep" runat="server" Text='<%#Eval("Dep")%>'></asp:Label></td>
                <td><asp:Label ID="LabelWarehouse" runat="server" Text='<%#Eval("Warehouse")%>'></asp:Label></td>
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label></td>                
                <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDN" runat="server" Text='<%#Eval("DN")%>'></asp:Label></td>
                <td><asp:Label ID="LabelRN" runat="server" Text='<%#Eval("RN")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnitPrice" runat="server" Text='<%#Eval("UnitPrice")%>'></asp:Label></td>
                <td><asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label></td>
                <td style="color:#FF0000;"><asp:HyperLink ID="HyperLinkRead" NavigateUrl='<%#"~/SM_Data/SM_WarehouseOUT_LL.aspx?FLAG=READ&&ID="+Eval("Code")%>' runat="server">查看</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    

        <hr />


    <div id="MTO" class="container">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <caption>MTO调整单</caption>
        <asp:Repeater ID="RepeaterMTO" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td><strong>日期</strong></td>
                <td><strong>审核状态</strong></td>
                <td><strong>单据编号</strong></td>
                <td><strong>计划员</strong></td>
                <td><strong>审核人</strong></td>
                <td><strong>审核日期</strong></td>                
                <td><strong>制单人</strong></td>                
                <td><strong>部门</strong></td>                
                <td><strong>物料代码</strong></td>
                <td><strong>物料名称</strong></td>
                <td><strong>型号规格</strong></td>
                <td><strong>从计划跟踪号</strong></td>
                <td><strong>仓库</strong></td>                
                <td><strong>仓位</strong></td>                  
                <td><strong>单位</strong></td>
                <td><strong>可调数量</strong></td>
                <td><strong>到计划跟踪号</strong></td>                
                <td><strong>调整数量</strong></td>                          
                <td><strong></strong></td>                                
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label></td>
                <td><asp:Label ID="LabelState" runat="server" Text='<%#convertMTO((string)Eval("State"))%>'></asp:Label></td>                
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
                <td><asp:Label ID="LabelPlaner" runat="server" Text='<%#Eval("Planer")%>'></asp:Label></td>
                <td><asp:Label ID="LabelVerifier" runat="server" Text='<%#Eval("Verifier")%>'></asp:Label></td>
                <td><asp:Label ID="LabelApproveDate" runat="server" Text='<%#Eval("ApproveDate")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDocument" runat="server" Text='<%#Eval("Document")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDep" runat="server" Text='<%#Eval("Dep")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label></td>
                <td><asp:Label ID="LabelPTCFrom" runat="server" Text='<%#Eval("PTCFrom")%>'></asp:Label></td>
                <td><asp:Label ID="LabelWarehouse" runat="server" Text='<%#Eval("Warehouse")%>'></asp:Label></td>
                <td><asp:Label ID="LabelPosition" runat="server" Text='<%#Eval("Position")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                <td><asp:Label ID="LabelWN" runat="server" Text='<%#Eval("WN")%>'></asp:Label></td>
                <td><asp:Label ID="LabelPTCTo" runat="server" Text='<%#Eval("PTCTo")%>'></asp:Label></td>
                <td><asp:Label ID="LabelAdjN" runat="server" Text='<%#Eval("AdjN")%>'></asp:Label></td>
                <td style="color:#FF0000;"><asp:HyperLink ID="HyperLinkRead" NavigateUrl='<%#"~/SM_Data/SM_Warehouse_MTOAdjust.aspx?FLAG=READ&&ID="+Eval("Code")%>' runat="server">查看</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    

        <hr />


 <%--   <div id="AL" class="container">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <caption>调拨单</caption>
        <asp:Repeater ID="RepeaterAL" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td><strong>制单日期</strong></td>
                <td><strong>审核状态</strong></td>
                <td><strong>单据编号</strong></td>
                <td><strong>制单人</strong></td>
                <td><strong>调出仓库</strong></td>
                <td><strong>调入仓库</strong></td>
                <td><strong>计划跟踪号</strong></td>
                <td><strong>物料代码</strong></td>
                <td><strong>物料名称</strong></td>
                <td><strong>型号规格</strong></td>
                <td><strong>材质</strong></td>
                <td><strong>单位</strong></td>
                <td><strong>数量</strong></td>
                <td><strong></strong></td>                                
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label></td>
                <td><asp:Label ID="LabelState" runat="server" Text='<%#convertAL((string)Eval("State"))%>'></asp:Label></td>                
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDocument" runat="server" Text='<%#Eval("Document")%>'></asp:Label></td>
                <td><asp:Label ID="LabelWarehouseOut" runat="server" Text='<%#Eval("WarehouseOut")%>'></asp:Label></td>
                <td><asp:Label ID="LabelWarehouseIn" runat="server" Text='<%#Eval("WarehouseIn")%>'></asp:Label></td>
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label></td>
                <td><asp:Label ID="LabelAttribute" runat="server" Text='<%#Eval("Attribute")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                <td><asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>'></asp:Label></td>
                <td style="color:#FF0000;"><asp:HyperLink ID="HyperLinkRead" NavigateUrl='<%#"~/SM_Data/SM_Warehouse_Allocation.aspx?FLAG=READ&&ID="+Eval("Code")%>' runat="server">查看</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    

        <hr />--%>

   
    <div id="Invoice" class="container">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <caption>发票</caption>
        <asp:Repeater ID="RepeaterInvoice" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td><strong>发票编号</strong></td>
                <td><strong>供应商</strong></td>
                <td><strong>开户银行</strong></td>
                <td><strong>发票号码</strong></td>
                <td><strong>凭证号</strong></td>
                <td><strong>记账人</strong></td>                            
                <td><strong>制单人</strong></td>
                <td><strong>登记日期</strong></td>
                <td><strong>计划跟踪号</strong></td>
                <td><strong>物料编码</strong></td>
                <td><strong>物料名称</strong></td>
                <td><strong>规格</strong></td>
                <td><strong>单位</strong></td>
                <td><strong>数量</strong></td>
                <td><strong></strong></td>                                
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
                <td><asp:Label ID="LabelSupplier" runat="server" Text='<%#Eval("Supplier")%>'></asp:Label></td>                
                <td><asp:Label ID="LabelBank" runat="server" Text='<%#Eval("Bank")%>'></asp:Label></td>
                <td><asp:Label ID="LabelInvoiceCode" runat="server" Text='<%#Eval("InvoiceCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelCertificateCode" runat="server" Text='<%#Eval("CertificateCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelAccounting" runat="server" Text='<%#Eval("Accounting")%>'></asp:Label></td>
                <td><asp:Label ID="LabelDocument" runat="server" Text='<%#Eval("Document")%>'></asp:Label></td>
                <td><asp:Label ID="LabelRegisterDate" runat="server" Text='<%#Eval("RegisterDate")%>'></asp:Label></td>
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                <td><asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>'></asp:Label></td>
                <td style="color:#FF0000;"><asp:HyperLink ID="HyperLinkRead" NavigateUrl='<%#"~/FM_Data/FM_Invoice.aspx?Action=View&&GI_CODE="+Eval("Code")%>' runat="server">查看</asp:HyperLink></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    


    </div>
    </div>

</asp:Content>
