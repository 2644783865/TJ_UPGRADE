<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_Xie_check_detail.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Xie_check_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    询比价单审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../PC_Data/PcJs/pricesearch.js" type="text/javascript" charset="GB2312"
        language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/xbjhidden.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

<%--    <script language="javascript" type="text/javascript">

    function openLink(obj)
    {
        var name;      
        name=obj.getElementsByTagName("td")[4].getElementsByTagName("span")[0].innerHTML;     
        windowopen("TBPC_ShowSupply.aspx?name=" + name);
       
    }
    </script>--%>

   <%-- <script language="javascript" type="text/javascript">
       Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args)
        {
            
        }
        function EndRequestHandler(sender, args)
        {
         var myST = new superTable("tab", {
	        cssSkin : "sDefault",
	        headerRows : 4,
	        fixedCols : 8,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
	            var ptc="<%=gloabptc%>";
                if(ptc!="")
                {
                    var col=2;
                    var table=document.getElementById("tab"); 
                    var tr=table.getElementsByTagName("tr"); 
                    for( i=4;i <tr.length-2;i++) 
                    {
                        var Qptc=tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML;
                        if(Qptc.substring(0,Qptc.indexOf("#"))!=""&&ptc.substring(0,ptc.indexOf("#"))=="")
                        {
                            Qptc=Qptc.substring(0,Qptc.indexOf("#"));
                        }
                        if(Qptc==ptc)
                        { 
                            tr[i].style.backgroundColor ='Orange'; 
                        }
                        else 
                        {
                            tr[i].style.backgroundColor ='';
                        }
                     }  
                 }      
	        },
	        onFinish : function () 
	        {   
	           for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
                {
                   
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                        var clicked = false;

                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                        return function () 
                              {
                                    if (clicked) 
                                    {
                                        dataRow.style.backgroundColor = "#ffffff";
                                        fixedRow.style.backgroundColor = "#C6E2FF";
                                        clicked = false;
                                    }
                                    else 
                                    {
                                        dataRow.style.backgroundColor = "#FFC125";
                                        fixedRow.style.backgroundColor = "#FFC125";
                                        clicked = true;
                                    }
                                }
                                }.call(this, i);
                            }
                         return this;
            }
        });

     }
    </script>--%>


    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                询比价单
                            </td>
                            <td align="right">
                               
                                <asp:Button ID="btn_edit" runat="server" Text="编辑" OnClick="btn_edit_Click" Enabled="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_delete" runat="server" Text="删除" ForeColor="Red" OnClick="btn_delete_Click" Enabled="false"  />
                                 &nbsp;&nbsp;
                                 <asp:Button ID="btn_cancel" runat="server" Text="单据取消" ForeColor="Red" OnClick="btn_cancel_Click" Enabled="false"  />
                                 &nbsp;&nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_Click"
                                    Enabled="false" Font-Size="Large" BorderStyle="Solid" BorderWidth="2px" BorderColor="Gray" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_split" runat="server" Text="拆分" />
                                <%--<asp:Button ID="btn_fanshen" runat="server" Text="反审" OnClick="btn_fanshen_Click"
                                    Enabled="false" />
                                 --%>
                                 <asp:Button ID="btn_shangcha" runat="server" Text="上查" OnClick="btn_shangcha_OnClick" />
                                 <asp:Button ID="btn_xiacha" runat="server" Text="下查" OnClick="btn_xiacha_OnClick" />
                             <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btn_split"
                                PopupControlID="palscwx"  Y="102" X="600" CancelControlID="close" ></asp:ModalPopupExtender>
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click"  />&nbsp;&nbsp;
                                <%--<asp:Button ID="btn_nosto" runat="server" Text="wukucun" OnClick="btn_nosto_Click" Visible="false"/>--%>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                   
                </div>
            </div>
        </div>
          <asp:Panel ID="palscwx" Style="display:none; border-style: solid; border-width: 1px;
                            border-color: blue; background-color: White; position: absolute;" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table width="200px">
                                        <tr>
                                            <td>
                                                <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                    font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                    <a id="close" style="background-color: #6699CC;
                                                        cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                        title="关闭">X</a>
                                                </div>
                                                    <br />
                                                <br />
                                            </td>
                                        </tr>
                                           <tr>
                                            <td>
                                                拆分：
                                            </td>
                                            <td colspan="4">
                                                <asp:DropDownList ID="ddl_split" runat="server" Width="50px">
                                                <asp:ListItem  Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                </asp:DropDownList>  
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>
                                            拆分<br />数量：
                                        </td>
                                        <td style="text-align:center">
                                        <asp:TextBox ID="txt_1" runat="server" Width="25px"  onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" ></asp:TextBox>
                                        </td>                                        
                                         <td style="text-align:center">
                                        <asp:TextBox ID="txt_2" runat="server" Width="25px" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"  ></asp:TextBox>
                                   
                                        </td>
                                         <td style="text-align:center">
                                        <asp:TextBox ID="txt_3" runat="server" Width="25px" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"  ></asp:TextBox>
                                        </td>
                                         <td style="text-align:center">
                                        <asp:TextBox ID="txt_4" runat="server" Width="25px" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"  ></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <br />
                                                <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_onclick"
                                                    UseSubmitBehavior="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnConfirm" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    询比价单编号:<asp:TextBox ID="TextBoxNO" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="right" visible="false">
                                    <asp:TextBox ID="TextBox1" runat="server" Text="3" Visible="false"></asp:TextBox>&nbsp;&nbsp;
                                   
                                </td>
                            </tr>
                        </table>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0">
                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="询比价单">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 480px">
                                        <div class="cpbox4 xscroll">
                                            <table id="tab" class="nowrap cptable fullwidth" width="100%">
                                                <asp:Repeater ID="checked_detailRepeater" runat="server" 
                                                    OnItemDataBound="checked_detailRepeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr id="row" align="center" style="background-color: #B9D3EE">
                                                            <td ID="wlxx" runat="server" colspan="18" rowspan="3">
                                                                外协件信息 
                                                            </td>
                                                            <td colspan="1" rowspan="3">
                                                                比价结果 
                                                            </td>
                                                            <td ID="GYS" runat="server" colspan="18" rowspan="1">
                                                                供应商信息 
                                                            </td>
                                                        </tr>
                                                        <tr align="center" style="background-color: #B9D3EE">
                                                            <td ID="gys1" runat="server" colspan="3" rowspan="1">
                                                                供应商1
                                                            </td>
                                                            <td ID="gys2" runat="server" colspan="3" rowspan="1">
                                                                供应商2
                                                            </td>
                                                            <td ID="gys3" runat="server" colspan="3" rowspan="1">
                                                                供应商3
                                                            </td>
                                                            <td ID="gys4" runat="server" colspan="3" rowspan="1">
                                                                供应商4
                                                            </td>
                                                            <td ID="gys5" runat="server" colspan="3" rowspan="1">
                                                                供应商5
                                                            </td>
                                                            <td ID="gys6" runat="server" colspan="3" rowspan="1">
                                                                供应商6
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="background-color: #B9D3EE">
                                                            <td ID="gysnm1" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" 
                                                                width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERANAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERAID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbA_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td ID="gysnm2" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" 
                                                                width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERBNAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERBID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbB_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td ID="gysnm3" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" 
                                                                width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERCNAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERCID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbC_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td ID="gysnm4" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" 
                                                                width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERDNAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERDID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbD_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td ID="gysnm5" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" 
                                                                width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERENAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIEREID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbE_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td ID="gysnm6" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" 
                                                                width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERFNAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERFID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbF_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center" style="background-color: #B9D3EE">
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td ID="td13" runat="server">
                                                                <strong>任务号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td ID="td3" runat="server">
                                                                <strong>图号</strong>
                                                            </td>
                                                            <td>
                                                            <strong>序号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>规格</strong>
                                                            </td>
                                                            <td ID="td4" runat="server">
                                                                <strong>材质</strong>
                                                            </td>
                                                            <td>
                                                                <strong>材料类型</strong>
                                                            </td>
                                                            <td>
                                                                <strong>外协类型</strong>
                                                            </td>
                                                            <td>
                                                            <strong>工艺流程</strong>
                                                            </td>
                                                            <td>
                                                            <strong>单重</strong>
                                                            </td>
                                                            <td ID="td6" runat="server">
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td ID="td7" runat="server">
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td ID="td8" runat="server">
                                                                <strong>金额</strong>
                                                            </td>
                                                            <td ID="td9" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td ID="td10" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <td ID="td11" runat="server">
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td>
                                                                <strong>比价结果</strong>
                                                            </td>
                                                            <td ID="dyc1" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec1" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="zz1" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc2" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec2" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="zz2" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc3" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec3" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="zz3" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc4" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec4" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="zz4" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc5" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec5" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="zz5" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc6" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec6" runat="server">
                                                                <strong>单价</strong>
                                                            </td>
                                                            <td ID="zz6" runat="server">
                                                                <strong>金额</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <FooterTemplate>
                                                        <tr align="center">
                                                            <td ID="foottd1" runat="server" colspan="14" rowspan="1">
                                                                汇总(RMB/元)
                                                            </td>
                                                            <%--<td id="tk1" runat="server">
                                                                <asp:Label visible="false" ID="Lb_zxnum" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="tk3" runat="server">
                                                                <asp:Label visible="false" ID="Label1" runat="server" Text=""></asp:Label>
                                                            </td>--%>
                                                            <td ID="tk2" runat="server">
                                                                <asp:Label ID="Lb_amount" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td colspan="4">
                                                                <asp:Label ID="Label1" runat="server" visible="false"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" visible="false"></asp:Label>
                                                            </td>
                                                            <%--   <td id="tk4" runat="server">
                                                                <asp:Label visible="false" ID="Label9" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="tk5" runat="server">
                                                              
                                                                <asp:Label visible="false" ID="Label10" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="tk6" runat="server">
                                                                <asp:Label visible="false" ID="Label2" runat="server" Text=""></asp:Label>
                                                            </td>--%>
                                                            <%--<td>
                                                                <asp:Label visible="false" ID="Label3" runat="server" Text=""></asp:Label>
                                                            </td>--%>
                                                            <td ID="Td1" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal11" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td ID="Td2" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal12" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td ID="Td3" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal13" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td ID="Td4" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal14" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td ID="Td5" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal15" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td ID="Td6" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal16" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <%-- <td>
                                                                <asp:Label visible="false" ID="sumzdjprice" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label visible="false" ID="sumzdjnum" runat="server"></asp:Label>
                                                            </td>--%>
                                                        </tr>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget">
                                                            <td>
                                                                <asp:CheckBox ID="CKBOX_SELECT" runat="server" BorderStyle="None" 
                                                                    Checked="false" CssClass="checkBoxCss" onclick="checkme(this)" />
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="rownum" runat="server" Text="<%#Container.ItemIndex + 1 %>"></asp:Label>
                                                                <%--<asp:Label ID="PIC_TASKID" runat="server" Text='<%#Eval("PIC_TASKID")%>' Visible="false"></asp:Label>--%>
                                                                <asp:Label ID="PIC_ID" runat="server" Text='<%#Eval("PIC_ID")%>' 
                                                                    Visible="false"></asp:Label>
                                                               <asp:Label ID="PIC_TASKID" runat="server" Text='<%#Eval("PIC_TASKID")%>' 
                                                                    Visible="false"></asp:Label>
                                                                     <asp:Label ID="MS_CODE" runat="server" Text='<%#Eval("MS_CODE")%>' 
                                                                    Visible="false"></asp:Label>
                                                                     <asp:Label ID="PTC" runat="server" Text='<%#Eval("PTC")%>' 
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td ID="tt13" runat="server">
                                                                <asp:Label ID="PIC_ENGID" runat="server" Text='<%#Eval("engid") %>'></asp:Label>
                                                                <%--<asp:Label ID="PIC_SALESCONTRACT" runat="server" Text='<%#Eval("pjid")%>' Visible="false" ></asp:Label>--%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                                            </td>
                                                            <td ID="tt3" runat="server">
                                                                <asp:Label ID="PIC_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>'></asp:Label>
                                                            </td>
                                                           <td ID="Td12" runat="server">
                                                                <asp:Label ID="PIC_ZONGXU" runat="server" Text='<%#Eval("PIC_ZONGXU")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MARGUIGE" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                                            </td>
                                                            <td ID="tt4" runat="server">
                                                                <asp:Label ID="PIC_MARCAIZ" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MASHAPE" runat="server" Text='<%#Eval("PIC_MASHAPE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_WXTYPE" runat="server" Text='<%#Eval("PIC_WXTYPE") %>'></asp:Label>
                                                            </td>
                                                              <td>
                                                                <asp:Label ID="MS_PROCESS" runat="server" Text='<%#Eval("MS_PROCESS")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MS_UWGHT" runat="server" Text='<%#Eval("MS_UWGHT") %>'></asp:Label>
                                                            </td>
                                                            <td ID="tt6" runat="server">
                                                                <asp:Label ID="PIC_ZXNUM" runat="server" Text='<%#Eval("marzxnum")%>'></asp:Label>
                                                            </td>
                                                            <td ID="tt7" runat="server">
                                                                <asp:Label ID="PIC_UNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                                            </td>
                                                            <td ID="tt8" runat="server">
                                                                <asp:Label ID="Amount" runat="server" Text='<%#Eval("detamount")%>'></asp:Label>
                                                            </td>
                                                            <td ID="tt9" runat="server">
                                                                <asp:Label ID="PIC_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                            </td>
                                                            <td ID="tt10" runat="server">
                                                                <asp:Label ID="PIC_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                            </td>
                                                            <td ID="tt11" runat="server">
                                                                <asp:Label ID="PIC_NOTE" runat="server" Text='<%#Eval("detailnote")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%--<asp:Label ID="supplierid" runat="server" Text='<%#Eval("supplierresid")%>' Visible="false"></asp:Label>--%>
                                                                <asp:Label ID="suppliernm" runat="server" Text='<%#Eval("supplierresnm")%>'></asp:Label>
                                                            </td>
                                                            <td ID="dycbj1" runat="server" width="60px">
                                                                <asp:Label ID="Label11" runat="server" Text='<%#Eval("qoutefstsa")%>'></asp:Label>
                                                            </td>
                                                            <td ID="decbj1" runat="server" width="60px">
                                                                <asp:Label ID="Label12" runat="server" Text='<%#Eval("qoutescdsa")%>'></asp:Label>
                                                            </td>
                                                            <td ID="zzbj1" runat="server" width="60px">
                                                                <asp:Label ID="Label13" runat="server" Text='<%#Eval("qoutelstsa")%>'></asp:Label>
                                                            </td>
                                                            <td ID="dycbj2" runat="server" width="60px">
                                                                <asp:Label ID="Label21" runat="server" Text='<%#Eval("qoutefstsb")%>'></asp:Label>
                                                            </td>
                                                            <td ID="decbj2" runat="server" width="60px">
                                                                <asp:Label ID="Label22" runat="server" Text='<%#Eval("qoutescdsb")%>'></asp:Label>
                                                            </td>
                                                            <td ID="zzbj2" runat="server" width="60px">
                                                                <asp:Label ID="Label23" runat="server" Text='<%#Eval("qoutelstsb")%>'></asp:Label>
                                                            </td>
                                                            <td ID="dycbj3" runat="server" width="60px">
                                                                <asp:Label ID="Label31" runat="server" Text='<%#Eval("qoutefstsc")%>'></asp:Label>
                                                            </td>
                                                            <td ID="decbj3" runat="server" width="60px">
                                                                <asp:Label ID="Label32" runat="server" Text='<%#Eval("qoutescdsc")%>'></asp:Label>
                                                            </td>
                                                            <td ID="zzbj3" runat="server" width="60px">
                                                                <asp:Label ID="Label33" runat="server" Text='<%#Eval("qoutelstsc")%>'></asp:Label>
                                                            </td>
                                                            <td ID="dycbj4" runat="server" width="60px">
                                                                <asp:Label ID="Label41" runat="server" Text='<%#Eval("qoutefstsd")%>'></asp:Label>
                                                            </td>
                                                            <td ID="decbj4" runat="server" width="60px">
                                                                <asp:Label ID="Label42" runat="server" Text='<%#Eval("qoutescdsd")%>'></asp:Label>
                                                            </td>
                                                            <td ID="zzbj4" runat="server" width="60px">
                                                                <asp:Label ID="Label43" runat="server" Text='<%#Eval("qoutelstsd")%>'></asp:Label>
                                                            </td>
                                                            <td ID="dycbj5" runat="server" width="60px">
                                                                <asp:Label ID="Label51" runat="server" Text='<%#Eval("qoutefstse")%>'></asp:Label>
                                                            </td>
                                                            <td ID="decbj5" runat="server" width="60px">
                                                                <asp:Label ID="Label52" runat="server" Text='<%#Eval("qoutescdse")%>'></asp:Label>
                                                            </td>
                                                            <td ID="zzbj5" runat="server" width="60px">
                                                                <asp:Label ID="Label53" runat="server" Text='<%#Eval("qoutelstse")%>'></asp:Label>
                                                            </td>
                                                            <td ID="dycbj6" runat="server" width="60px">
                                                                <asp:Label ID="Label61" runat="server" Text='<%#Eval("qoutefstsf")%>'></asp:Label>
                                                            </td>
                                                            <td ID="decbj6" runat="server" width="60px">
                                                                <asp:Label ID="Label62" runat="server" Text='<%#Eval("qoutescdsf")%>'></asp:Label>
                                                            </td>
                                                            <td ID="zzbj6" runat="server" width="60px">
                                                                <asp:Label ID="Label63" runat="server" Text='<%#Eval("qoutelstsf")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="35">
                                                        <asp:Panel ID="NoDataPane1" runat="server" Visible="False">
                                                            没有记录！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>

      <script language="javascript" type="text/javascript">
   
                                            var k=8;
                                            var j=15;
                                            var myST = new superTable("tab", 
        {
	        cssSkin : "sDefault",
	        headerRows : 4,
	        fixedCols : k,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
	            var ptc="<%=gloabptc%>";
                if(ptc!="")
                {
                    var col=2;
                    var table=document.getElementById("tab"); 
                    var tr=table.getElementsByTagName("tr"); 
                    for( i=4;i <tr.length-2;i++) 
                    { 
                        var Qptc=tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML;
                        if(Qptc.substring(0,Qptc.indexOf("#"))!=""&&ptc.substring(0,ptc.indexOf("#"))=="")
                        {
                            Qptc=Qptc.substring(0,Qptc.indexOf("#"));
                        }
                        if(Qptc==ptc)
                        { 
                            tr[i].style.backgroundColor ='Orange'; 
                        }
                        else 
                        {
                            tr[i].style.backgroundColor ='';
                        }
                     }  
                 }      
//		        this.start = new Date();
	        },
	        onFinish : function () 
	        {   
	           for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
                {
                   
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                        var clicked = false;

                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                        return function () 
                              {
                                    if (clicked) 
                                    {
                                        dataRow.style.backgroundColor = "#ffffff";
                                        fixedRow.style.backgroundColor = "#C6E2FF";
                                        clicked = false;
                                    }
                                    else 
                                    {
                                        dataRow.style.backgroundColor = "#FFC125";
                                        fixedRow.style.backgroundColor = "#FFC125";
                                        clicked = true;
                                    }
                                }
                                }.call(this, i);
                            }
                         return this;
            }
        });
                                            </script>

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="比价结果" TabIndex="1">
                                <ContentTemplate>
                                    <div class="fixbox" style="height: 440px">
                                        <table ID="Table1" class="nowrap fixtable fullwidth" width="100%">
                                            <asp:Repeater ID="tbpc_comparepriceresultRepeater" runat="server" 
                                                OnItemDataBound="tbpc_comparepriceresultRepeater_ItemDataBound">
                                                <HeaderTemplate>
                                                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                        <%--<td>
                                                            &nbsp;
                                                        </td>--%>
                                                        <td>
                                                            <strong>行号</strong>
                                                        </td>
                                                        <td>
                                                            <strong>合同号</strong>
                                                        </td>
                                                        <td>
                                                            <strong>名称</strong>
                                                        </td>
                                                        <td>
                                                            <strong>图号</strong>
                                                        </td>
                                                        <td>
                                                        <strong>序号</strong>
                                                        </td>
                                                        <td>
                                                            <strong>规格</strong>
                                                        </td>
                                                        <td>
                                                            <strong>材质</strong>
                                                        </td>
                                                        <td>
                                                            <strong>材料类型</strong>
                                                        </td>
                                                        <td>
                                                            <strong>外协类型</strong>
                                                        </td>
                                                        <td>
                                                        <strong>单重</strong>
                                                        </td>
                                                        <td>
                                                            <strong>国标</strong>
                                                        </td>
                                                        <td ID="Td1" runat="server" visible="false">
                                                            <strong>数量</strong>
                                                        </td>
                                                        <td>
                                                            <strong>数量</strong>
                                                        </td>
                                                        <td>
                                                            <strong>单位</strong>
                                                        </td>
                                                        <td>
                                                            <strong>供应商</strong>
                                                        </td>
                                                        <td>
                                                            <strong>交货期</strong>
                                                        </td>
                                                        <td>
                                                            <strong>单价(含税)/元</strong>
                                                        </td>
                                                        <td>
                                                            <strong>金额(含税)/元</strong>
                                                        </td>
                                                        <td>
                                                            <strong>税率(%)</strong>
                                                        </td>
                                                        <%--
                                                        <td id="fzn1" runat="server">
                                                            <strong>辅助数量</strong>
                                                        </td>
                                                        <td id="zxfzn1" runat="server">
                                                            <strong>执行辅助数量</strong>
                                                        </td>
                                                        <td id="fzu1" runat="server">
                                                            <strong>辅助单位</strong>
                                                        </td>--%>
                                                        <td>
                                                            <strong>长度</strong>
                                                        </td>
                                                        <td>
                                                            <strong>宽度</strong>
                                                        </td>
                                                        <td>
                                                            <strong>备注</strong>
                                                        </td>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="baseGadget" onmouseout="this.className='baseGadget'" 
                                                        onmouseover="this.className='highlight'">
                                                       <%-- <td>
                                                            <asp:CheckBox ID="CKBOX_SELECT" runat="server" BorderStyle="None" 
                                                                Checked="false" CssClass="checkBoxCss" />
                                                        </td>--%>
                                                        <td>
                                                            <asp:Label ID="rownum" runat="server" Text="<%#Container.ItemIndex + 1 %>"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_SALESCONTRACT" runat="server" Text='<%#Eval("pjid")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_MARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                         <td >
                                                                <asp:Label ID="PIC_ZONGXU" runat="server" Text='<%#Eval("PIC_ZONGXU")%>'></asp:Label>
                                                            </td>
                                                        <td>
                                                            <asp:Label ID="PIC_MARGUIGE" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_MARCAIZ" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_MASHAPE" runat="server" Text='<%#Eval("PIC_MASHAPE")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_WXTYPE" runat="server" Text='<%#Eval("PIC_WXTYPE")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                                <asp:Label ID="MS_UWGHT" runat="server" Text='<%#Eval("MS_UWGHT") %>'></asp:Label>
                                                            </td>
                                                        
                                                        <td>
                                                            <asp:Label ID="PIC_GUOBIAO" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td ID="Td2" runat="server" visible="false">
                                                            <asp:Label ID="PIC_QUANTITY" runat="server" Text='<%#Eval("marnum")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_ZXNUM" runat="server" Text='<%#Eval("marzxnum")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_UNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="supplierid" runat="server" Text='<%#Eval("supplierresid")%>' 
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="suppliernm" runat="server" Text='<%#Eval("supplierresnm")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server"></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_PRICE" runat="server" Text='<%#Eval("price")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Amount" runat="server" Text='<%#Eval("detamount")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_SHUILV" runat="server" Text='<%#Eval("shuilv")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <%-- <td id="fzn2" runat="server">
                                                            <asp:Label ID="PIC_FZNUM" runat="server" Text='<%#Eval("marfznum")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td id="zxfzn2" runat="server">
                                                            <asp:Label ID="PIC_ZXFUNUM" runat="server" Text='<%#Eval("marzxfznum")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td id="fzu2" runat="server">
                                                            <asp:Label ID="PIC_FZUNIT" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                                        </td>--%>
                                                        <td>
                                                            <asp:Label ID="PIC_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_NOTE" runat="server" Text='<%#Eval("detailnote")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr align="center">
                                                        <td colspan="13" rowspan="1">
                                                            汇总(RMB/元)
                                                        </td>
                                                        <td ID="Td3" runat="server" visible="false">
                                                            <asp:Label ID="totalnum" runat="server" Text="0" visible="false"></asp:Label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Label ID="totalzxnum" runat="server" Text="0" visible="false"></asp:Label>
                                                        </td>
                                                        <td colspan="1">
                                                            <asp:Label ID="totalamount" runat="server" Text="0"></asp:Label>
                                                        </td>
                                                        <%-- <td rowspan="1">
                                                            <asp:Label visible="false" ID="totalamount" runat="server" Text="0"></asp:Label>
                                                        </td>--%>
                                                        <td ID="foot" runat="server" colspan="7">
                                                        </td>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td align="center" colspan="27">
                                                    <asp:Panel ID="NoDataPane2" runat="server" Visible="false">
                                                        没有数据！</asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="评审结果" TabIndex="2">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 450px; overflow: auto">
                                        <asp:Panel ID="Pan_shenheren" runat="server">
                                            <table width="100%">
                                                <asp:Panel ID="Panel_zd" runat="server">
                                                    <tr>
                                                        <td>
                                                            制单意见:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_zdanyj" runat="server" Columns="100" Rows="6" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr ID="Tr1" runat="server" visible="false">
                                                        <td>
                                                            审核人数:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="tb_pnum" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2" style="border-bottom: solid 1px black;">
                                                            制单人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_zdanren" runat="server" CssClass="text1style" 
                                                                Enabled="False"></asp:TextBox>
                                                            <asp:TextBox ID="TB_zdanrenid" runat="server" Visible="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_tjiaot" runat="server" CssClass="text1style" 
                                                                Enabled="False"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="Pan_shenheren1" runat="server" Enabled="false">
                                                    <tr>
                                                        <td>
                                                            主管意见:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheyj1" runat="server" Columns="100" Rows="3" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            审核结论:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                            <asp:RadioButton ID="Rad_tongyi1" runat="server" AutoPostBack="true" 
                                                                GroupName="shenhe1" OnCheckedChanged="Rad_tongyi1_checkedchanged" Text="同意" 
                                                                TextAlign="Right" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="Rad_butongyi1" runat="server" GroupName="shenhe1" 
                                                                Text="拒绝" TextAlign="Right" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2" style="border-bottom: solid 1px black;">
                                                            审核人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheren1" runat="server" CssClass="text1style" 
                                                                Enabled="false"></asp:TextBox>
                                                            <asp:TextBox ID="Tb_shenherencode1" runat="server" Visible="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenhet1" runat="server" 
                                                                CssClass="text1style" Enabled="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="Pan_shenheren2" runat="server" Enabled="false">
                                                    <tr>
                                                        <td>
                                                            经理意见:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheyj2" runat="server" Columns="100" Rows="3" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            审核结论:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                            <asp:RadioButton ID="Rad_tongyi2" runat="server" AutoPostBack="true" 
                                                                GroupName="shenhe2" OnCheckedChanged="Rad_tongyi2_checkedchanged" Text="同意" 
                                                                TextAlign="Right" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="Rad_butongyi2" runat="server" GroupName="shenhe2" 
                                                                Text="拒绝" TextAlign="Right" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2" style="border-bottom: solid 1px black;">
                                                            审核人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheren2" runat="server" CssClass="text1style" 
                                                                Enabled="false"></asp:TextBox>
                                                            <asp:TextBox ID="Tb_shenherencode2" runat="server" Visible="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenhet2" runat="server" 
                                                                CssClass="text1style" Enabled="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </cc1:TabContainer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
