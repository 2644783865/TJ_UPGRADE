<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="SM_Warehouse_MTONotes.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_MTONotes" %>


<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                &nbsp;&nbsp;&nbsp;<asp:Label ID="LabelMessage" runat="server"></asp:Label>
            </td>
            <td align="left">

            </td>
            <td align="right">
                调整状态：<asp:DropDownList ID="DropDownListState" runat="server" 
                AutoPostBack="true" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged">
                <asp:ListItem Text="未调整" Value="1"></asp:ListItem>
                <asp:ListItem Text="已调整" Value="2"></asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;&nbsp;           
                <asp:Button ID="Readed" runat="server" Text="调整！" OnClick="Readed_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    
<script type="text/javascript" language="javascript">

    function allsel()
{
   var  table=document.getElementById("GridView1");
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
    }
}

function cancelsel()
{
   var  table=document.getElementById("GridView1");
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
    }
}

function consel()
{
    table=document.getElementById("GridView1");
    tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
        {
            if(obj.checked)
            {
                obj.checked=true;
                for(var j=i+1;j<(tr.length-1);j++)
                {
                    var nextobj=tr[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    if(nextobj.type.toLowerCase()=="checkbox" && nextobj.value!="")
                    {
                        if(nextobj.checked)
                        {
                            for(var k=i+1;k<j+1;k++)
                            {
                                tr[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                            }
                        }
                    }  
                }
            }
        }
    }
}



</script>

        <asp:Panel ID="Panel_Operation" runat="server" >
           &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input id="continue" type="button" value="连选" onclick="consel()"/>
           &nbsp;&nbsp;&nbsp;<input id="cancel" type="button" value="取消" onclick="cancelsel()"/>
    </asp:Panel>
    
    <asp:Panel ID="Panel1" runat="server"  style="overflow:auto;position:static; margin:2px" Width="98%" Height="380px">
    
    <table id="GridView1" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td width="15px"><strong></strong></td>
                <td width="15px"><strong></strong></td>
                <td width="100px"><strong>变更批号</strong></td>
                <td width="150px"><strong>计划跟踪号</strong></td>  
                <td width="60px"><strong>物料编码</strong></td>
                <td width="120px"><strong>物料名称</strong></td>
                <td width="120px"><strong>型号规格</strong></td>
                <td width="60px"><strong>材质</strong></td>
                <td width="80px"><strong>国标</strong></td>
                <td width="60px"><strong>单位</strong></td>                
                <td width="60px"><strong>调整数量</strong></td>                
                <td width="80px"><strong>调整张(支)</strong></td>                
                <td width="60px"><strong>计划员</strong></td>
                <td width="70px"><strong>提交时间</strong></td>
                <td width="100px"><strong>备注</strong></td>                                            
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td width="15px"><asp:CheckBox ID="CheckBox1" runat="server" /></td>
                <td width="15px"><%#Container.ItemIndex+1%><asp:Label ID="LabelID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label></td>
                
                <td><asp:Label ID="LabelCHCODE" runat="server" Text='<%#Eval("CHCODE")%>' width="100px"></asp:Label>  </td>
                
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>' width="150px"></asp:Label></td>
                <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>'  width="120px"></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'  width="120px"></asp:Label></td>
                <td><asp:Label ID="LabelAttribute" runat="server" Text='<%#Eval("Attribute")%>'  width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelGB" runat="server" Text='<%#Eval("GB")%>' width="80px"></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelWeight" runat="server" Text='<%#Eval("Weight")%>' width="60px"></asp:Label></td>                
                <td><asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>' width="80px"></asp:Label></td>
                <td><asp:Label ID="LabelClerk" runat="server" Text='<%#Eval("Clerk")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>' width="70px"></asp:Label></td>
                <td><asp:Label ID="LabelNote" runat="server" Text='<%#Eval("Note")%>' width="100px"></asp:Label></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">没有技术部转备库通知!</asp:Panel>
    </table>
    </asp:Panel>
    
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->    

</asp:Content>
