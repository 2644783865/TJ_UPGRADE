<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_marstouseallGB.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_marstouseallGB"
    Title="备库占用信息" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="commanrepeater.css" rel="stylesheet" />
    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
    <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>
    <script src="PcJs/pricesearch.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <%-- <script src="../JS/add.js" type="text/javascript" charset="GB2312" language="javascript"></script>--%>

    <script language="javascript" type="text/javascript">
      
//        function getmarzy(obj)
//        {
//           var marid;
//           var row=obj;
//           var marid=row.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
//           var marcz=row.getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML;
//           windowopen("PC_Date_silimarmarshow.aspx?marnm=物料&marid=" + marid+"&marcz="+marcz);
////         window.showModalDialog("PC_Date_silimarmarshow.aspx?marid="+marid,'',"dialogHeight:600px;dialogWidth:800px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no"); 
//        }
       function String.prototype.Trim()   
       {
             return this.replace(/\s+/g,"");
       }  
        function getsum(no1,no2)
        {
             var table=document.getElementById("tab"); 
             var tr=table.getElementsByTagName("tr"); 
             var pattem=/^\d+(\.\d+)?$/; 
             var a;
             var sum=0;
             for( i=1;i <tr.length-2;i++) 
             {
                 if(pattem.test(tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value.Trim()))
                   { 
                        tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value.Trim();
                        a=parseFloat(tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value); 
                        if(a<0)
                        {
                            tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value=-tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value;
                            a=-tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value;
                        }
                    }
                    else 
                    {
                        a=0; 
//                        tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value="0";
                    }
                    sum+=a;
            }
            tr[tr.length-2].getElementsByTagName("td")[no1-no2].getElementsByTagName("input")[0].value=sum.toFixed(3);

        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btn_concel" runat="server" Text="取消" OnClick="btn_concel_Click" />&nbsp;
                                <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />&nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div style="overflow: auto; width: 100%">
                    <div class="cpbox xscroll">
                        <div width="100%">
                            <table width="100%">
                                <tr>
                                    <td style="font-size: small; text-align: center;" colspan="4">
                                        备库占用信息
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;单&nbsp;&nbsp;&nbsp;&nbsp;人:
                                        <asp:TextBox ID="tb_peoname" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="tb_peoid" runat="server" Text="" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;单&nbsp;&nbsp;日&nbsp;&nbsp;期:<asp:TextBox ID="Tb_shijian"
                                            runat="server" Text="" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                       &nbsp;&nbsp;&nbsp;批&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号:<asp:TextBox ID="TextBox_pid"
                                            runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;生&nbsp;&nbsp;产&nbsp;&nbsp;制&nbsp;&nbsp;号:
                                        <asp:TextBox ID="tb_zh" runat="server" Text="" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目:<asp:TextBox
                                            ID="tb_pjinfo" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_pjname" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                        &nbsp;&nbsp;&nbsp;工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;程:<asp:TextBox
                                            ID="tb_enginfo" runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                        <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_engname" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="tab" width="100%" class="nowrap cptable fullwidth">
                            <asp:Repeater ID="tbpc_gbzyRepeater" runat="server" OnItemDataBound="tbpc_gbzyRepeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>计划号</strong>
                                        </td>
                                        <td>
                                            <strong>物料编码</strong>
                                        </td>
                                        <td>
                                            <strong>名称</strong>
                                        </td>
                                        <td>
                                            <strong>规格</strong>
                                        </td>
                                        <td>
                                            <strong>材质</strong>
                                        </td>
                                        <td>
                                            <strong>国标</strong>
                                        </td>
                                        <td>
                                            <strong>单位</strong>
                                        </td>
                                        <td>
                                            <strong>数量</strong>
                                        </td>
                                        <td>
                                            <strong>辅助数量</strong>
                                        </td>
                                        <td>
                                            <strong>辅助单位</strong>
                                        </td>
                                        <td>
                                            <strong>长度</strong>
                                        </td>
                                        <td>
                                            <strong>宽度</strong>
                                        </td>
                                        <td>
                                            <strong>占用数量</strong>
                                        </td>
                                        <td>
                                            <strong>占用辅助数量</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td>
                                            <strong>状态</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" >
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="ROWBGSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_FZNUM" runat="server" Text='<%#Eval("fznum")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_FZNUNIT" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PUR_USENUM" runat="server" Text='<%#Eval("usenum")%>' onblur="getsum(13,7)" onkeydown="grControlFocus(this)"></asp:TextBox>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PUR_USEFZNUM" runat="server" Text='<%#Eval("usefznum")%>' onblur="getsum(14,7)" onkeydown="grControlFocus(this)"></asp:TextBox>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PUR_NOTE" runat="server" Text='<%#Eval("allnote")%>'></asp:TextBox>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_STATETEXT" runat="server" Text='<%#get_all_state(Eval("allstata").ToString())%>'></asp:Label>
                                            <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("allstata")%>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr align="center">
                                        <td rowspan="1" colspan="8">
                                            合计:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_ntotal" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_fztotal" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_rpntotal" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_rpfztotal" runat="server" BorderStyle="None" Enabled="False"
                                                BackColor="#EFF3FB"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="17" align="center">
                                    <asp:Panel ID="NoDataPane1" runat="server" Visible="false">
                                        没有数据！</asp:Panel>
                                </td>
                            </tr>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
